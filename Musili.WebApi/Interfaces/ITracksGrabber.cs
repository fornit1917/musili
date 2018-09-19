using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces
{
    public interface ITracksGrabber
    {
         Task<List<Track>> GrabRandomTracksAsync(TracksSource tracksSource);
    }
}