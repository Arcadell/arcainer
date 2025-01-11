using Application.Interfaces.Commands;
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
