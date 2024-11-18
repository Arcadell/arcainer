using System.Text;
using Application.Interfaces;
using Domain.Dtos;
using Domain.Filters;
using Domain.Filters.SearchTypes;
using Domain.Models;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Commands;
using Ductus.FluentDocker.Services;

namespace FluentDocker.Commands
{
    public class ContainerCommands(IHostService client) : IContainerCommands
    {
        public void CreateStack(CreateContainerDto createContainerDto)
        {
            try
            {
                var currentPath = Directory.GetCurrentDirectory();
                var composesPath = Path.Combine(currentPath, "composes");
                if (!Directory.Exists(composesPath)) {  Directory.CreateDirectory(composesPath);  }

                var newComposeDirectoryPath = Path.Combine(composesPath, createContainerDto.Name);
                if (!Directory.Exists(newComposeDirectoryPath)) { Directory.CreateDirectory(newComposeDirectoryPath); }

                var newComposePath = Path.Combine(newComposeDirectoryPath, "docker-compose.yml");

                // Create the file, or overwrite if the file exists.
                using (var fs = File.Create(newComposePath))
                {
                    var info = new UTF8Encoding(true).GetBytes(createContainerDto.Compose);
                    fs.Write(info, 0, info.Length);
                }

                if (createContainerDto.StartOnCreate)
                {
                    new Builder()
                        .UseContainer()
                        .UseCompose()
                        .FromFile(newComposePath)
                        .RemoveOrphans()
                        .Build().Start();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Container> GetContainers(ContainerFilter containerFilter)
        {
            try
            {
                var containers = client.GetContainers();
                var list = containers.Select(x => new Container() { Id = x.Id, Name = x.Name, State = x.State.ToString() }).Where(x => FilterContainer(x, containerFilter)).ToList();

                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ContainersStack> GetStacks(string? stackName = null)
        {
            try
            {
                var currentPath = Directory.GetCurrentDirectory();
                var composesPath = Path.Combine(currentPath, "composes");
                if (!Directory.Exists(composesPath)) { Directory.CreateDirectory(composesPath); }

                if(!string.IsNullOrEmpty(stackName))
                {
                    var stackNameFolderPath = Path.Combine(composesPath, stackName);
                    var stackComposePath = Path.Combine(stackNameFolderPath, "docker-compose.yml");
                    if (!Directory.Exists(stackNameFolderPath)) { throw new Exception("Directory does not exists");  }
                    if (!File.Exists(stackComposePath)) { throw new Exception("Compose does not exists"); }

                    using var reader = new StreamReader(stackComposePath);
                    var composeValue = reader.ReadToEnd();

                    var localList = new List<ContainersStack>();
                    localList.Add(new ContainersStack() { Name = stackName, DockerCompose = composeValue });
                    return localList;
                }

                var composes = Directory.GetDirectories(composesPath).Select(x => new DirectoryInfo(x).Name);
                var list = composes.Select(x => new ContainersStack() { Name = x, DockerCompose = string.Empty }).ToList();

                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BaseResponse> StartContainers(List<string> ids)
        {
            var baseResponses = new List<BaseResponse>();
            ids.ForEach(id =>
            {
                try
                {
                    client.Host.Start(id, client.Certificates);
                    baseResponses.Add(new BaseResponse() { Id = id });
                }
                catch (Exception e)
                {
                    baseResponses.Add(new BaseResponse() { Id = id, Error = true, ErrorMessage = e.Message });
                }
            });
            return baseResponses;
        }

        public List<BaseResponse> StopContainers(List<string> ids)
        {
            var baseResponses = new List<BaseResponse>();
            ids.ForEach(id =>
            {
                try
                {
                    client.Host.Stop(id, null, client.Certificates);
                    baseResponses.Add(new BaseResponse() { Id = id });
                }
                catch (Exception e)
                {
                    baseResponses.Add(new BaseResponse() { Id = id, Error = true, ErrorMessage = e.Message });
                }
            });
            return baseResponses;   
        }

        public List<BaseResponse> DeleteContainers(List<string> ids)
        {
            var baseResponses = new List<BaseResponse>();
            ids.ForEach(id =>
            {
                try
                {
                    client.Host.RemoveContainer(id, true, true, null, client.Certificates);
                    baseResponses.Add(new BaseResponse() { Id = id });
                }
                catch (Exception e)
                {
                    baseResponses.Add(new BaseResponse() { Id = id, Error = true, ErrorMessage = e.Message });
                }
            });
            return baseResponses;
        }

        public List<BaseResponse> DeleteStacks(List<string> stackNames)
        {
            var currentPath = Directory.GetCurrentDirectory();
            var composesPath = Path.Combine(currentPath, "composes");
            if (!Directory.Exists(composesPath)) { Directory.CreateDirectory(composesPath); }
            
            var baseResponses = new List<BaseResponse>();
            stackNames.ForEach(stackName =>
            {
                try
                {
                    var stackNameFolderPath = Path.Combine(composesPath, stackName);
                    var stackComposePath = Path.Combine(stackNameFolderPath, "docker-compose.yml");
                    if (!Directory.Exists(stackNameFolderPath)) { throw new Exception("Directory does not exists");  }
                    if (!File.Exists(stackComposePath)) { throw new Exception("Compose does not exists"); }
                
                    Directory.Delete(stackNameFolderPath, true);
                    baseResponses.Add(new BaseResponse() { Id = stackName });
                }
                catch (Exception e)
                {
                    baseResponses.Add(new BaseResponse() { Id = stackName, Error = true, ErrorMessage = e.Message });
                }
            });

            return baseResponses;
        }

        #region PRIVATE FUNCTION
        private bool FilterContainer(Container container, ContainerFilter containerFilter)
        {
            bool result = true;
            foreach (var idFilter in containerFilter.Id)
            {
                result = false;
                switch (idFilter.Type)
                {
                    case SearchType.Equal:
                        if (container.Id == idFilter.Value)
                            return true;
                        break;

                    case SearchType.Like:
                        if (container.Id.Contains(idFilter.Value))
                            return true;
                        break;
                }
            }

            foreach (var nameFilter in containerFilter.Name)
            {
                result = false;
                switch (nameFilter.Type)
                {
                    case SearchType.Equal:
                        if (container.Name == nameFilter.Value)
                            return true;
                        break;

                    case SearchType.Like:
                        if (container.Name.Contains(nameFilter.Value))
                            return true;
                        break;
                }
            }

            foreach (var stateFilter in containerFilter.State)
            {
                result = false;
                switch (stateFilter.Type)
                {
                    case SearchType.Equal:
                        if (container.State == stateFilter.Value)
                            return true;
                        break;

                    case SearchType.Like:
                        if (container.State.Contains(stateFilter.Value))
                            return true;
                        break;
                }
            }

            return result;
        }
        #endregion
    }
}
