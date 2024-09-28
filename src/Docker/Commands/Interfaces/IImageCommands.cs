using Domain.Filters;
using Domain.Models;

namespace Docker.Commands.Interfaces
{
    public interface IImageCommands
    {
        List<Image> GetImages(ImageFilter imageFilter);
    }
}