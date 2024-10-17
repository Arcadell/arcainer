using Application.Interfaces;
using Domain.Dtos;
using Domain.Filters;
using Domain.Filters.SearchTypes;
using Domain.Models;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Commands;
using System.IO;
using System.Text;

namespace Docker.Commands
{
    public class ContainerCommands(IHostService client) : IContainerCommands
    {
        public void CreateContainer(CreateContainerDto createContainerDto)
        {
            string currentPath = Directory.GetCurrentDirectory();
            string composesPath = Path.Combine(currentPath, "composes");

            try
            {
                if (!Directory.Exists(composesPath)) { Directory.CreateDirectory(composesPath); }

                string newComposeDirectoryPath = Path.Combine(composesPath, createContainerDto.Name);

                if (Directory.Exists(newComposeDirectoryPath)) { return; }
                Directory.CreateDirectory(newComposeDirectoryPath);

                string newComposePath = Path.Combine(newComposeDirectoryPath, "docker-compose.yml");

                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(newComposePath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(createContainerDto.Compose);
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving docker compose file", e.ToString());
            }
            finally { }
        }

        public List<Container> GetContainers(ContainerFilter containerFilter)
        {
            var containers = client.GetContainers(true);
            var list = containers.Select(x => new Container() { Id = x.Id, Name = x.Name, State = x.State.ToString() }).Where(x => FilterContainer(x, containerFilter)).ToList();

            return list;
        }

        public Task StartContainers(List<string> ids)
        {
            ids.ForEach(id => { client.Host.Start(id, client.Certificates); });
            return Task.CompletedTask;
        }

        public Task StopContainers(List<string> ids)
        {
            ids.ForEach(id => { client.Host.Stop(id, null, client.Certificates); });
            return Task.CompletedTask;
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
