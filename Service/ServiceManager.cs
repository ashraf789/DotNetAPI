using Service.ServiceImp;
using Service.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Service
{
    public sealed class ServiceManager
    {
        private readonly ConcurrentDictionary<string, object> _services = new ConcurrentDictionary<string, object>();
        private static readonly Lazy<ServiceManager> Manager = new Lazy<ServiceManager>(() => new ServiceManager());

        public static ServiceManager RepositoryService => Manager.Value;

        // prevent to initializing this class.
        private ServiceManager()
        {

        }

        private T GetService<T>(){
            if (!typeof(IBaseService).IsAssignableFrom(typeof(T)))
            {
                throw new TypeAccessException("Only IBaseService accepted");
            }

            var type = typeof(T).FullName;
            if (_services.ContainsKey(type))
            {
                return (T)_services[type];
            }

            var service = new Lazy<T>(LazyThreadSafetyMode.PublicationOnly);
            _services.TryAdd(type, service.Value);

            return service.Value;
        }

        private IUserService _UserService;
        public IUserService UserService => _UserService ?? (_UserService = GetService<UserService>());
    } 
}