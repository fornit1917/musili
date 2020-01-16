using System.Threading;
using Musili.ApiApp.Models;

namespace Musili.ApiApp.Services {
    public interface ISemaphores {
         SemaphoreSlim GetSemaphoreForService(TracksSourceService service);
    }
}