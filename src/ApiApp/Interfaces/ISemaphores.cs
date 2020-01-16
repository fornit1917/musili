using System.Threading;
using Musili.ApiApp.Models;

namespace Musili.ApiApp.Interfaces {
    public interface ISemaphores {
         SemaphoreSlim GetSemaphoreForService(TracksSourceService service);
    }
}