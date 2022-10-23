using System;
using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.DatabaseLayer;
using Unity;

namespace EaseLogMeIn.WebApp
{
    public static class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              Resolver.RegisterTypes(container);
              return container;
          });
        public static IUnityContainer Container => container.Value;
        #endregion

    }
}