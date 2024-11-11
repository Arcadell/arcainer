using Application.Interfaces;
using Domain.Filters;
using Domain.Filters.SearchTypes;
using Domain.Models;
using Ductus.FluentDocker.Services;

namespace FluentDocker.Commands
{
    public class ImageCommands(IHostService client) : IImageCommands
    {
        public List<Image> GetImages(ImageFilter imageFilter)
        {
            var images = client.GetImages();
            var list = images.Select(x => new Image() { Id = x.Id }).Where(x => FilterImage(x, imageFilter)).ToList();

            return list;
        }

        public List<BaseResponse> DeleteImages(List<string> ids)
        {
            try
            {
                var images = client.GetImages();
                var containerImageServices = images.Where(x => ids.Contains(x.Id));
                
                var baseResponse = new List<BaseResponse>();
                foreach (var containerImageService in containerImageServices)
                {
                    try
                    {
                        containerImageService.Remove(false);
                        baseResponse.Add(new BaseResponse { Id = containerImageService.Id });
                    }
                    catch (Exception e)
                    {
                        baseResponse.Add(new BaseResponse { Id = containerImageService.Id, Error = true, ErrorMessage = e.Message });
                    }
                }

                return baseResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #region PRIVATE FUNCTION
        private bool FilterImage(Image image, ImageFilter imageFilter) {
            bool result = true;
            foreach (var idFilter in imageFilter.Id)
            {
                result = false;
                switch (idFilter.Type)
                {
                    case SearchType.Equal:
                        if (image.Id == idFilter.Value)
                            return true;
                        break;

                    case SearchType.Like:
                        if (image.Id.Contains(idFilter.Value))
                            return true;
                        break;
                }
            }

            return result;
        }
        #endregion
    }
}
