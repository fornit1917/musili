using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces
{
    public interface ITracksGrabber
    {
         Task<Track[]> GrabRandomTracksAsync(TracksSource tracksSource);
    }
}