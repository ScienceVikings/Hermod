using System.Threading.Tasks;
using Hermod.Core.Models;

namespace Hermod.Core.Services
{
    public interface ISocialMediaSite
    {
        Task Post(RssFeedItem item);
    }
}