using Application.Interfaces;
using Docker.DotNet;
using Docker.DotNet.Models;
using Domain.Filters;
using Domain.Filters.SearchTypes;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Commands
{
    public class ImageCommands(IDockerClient client) : IImageCommands
    {
        public List<Image> GetImages(ImageFilter imageFilter)
        {
            var images = client.Images.ListImagesAsync(new ImagesListParameters()).Result;
            var list = images.Select(x => new Image() { Id = x.ID }).Where(x => FilterImage(x, imageFilter)).ToList();

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
