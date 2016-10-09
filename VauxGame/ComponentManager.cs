using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using VauxGame.Handlers;

namespace VauxGame
{
    class ComponentManager
    {
        private static ComponentManager _instance;

        public static ComponentManager Instance => _instance ?? (_instance = new ComponentManager());

        #region - Fields - 

        private IContainer _container;
        private ContainerBuilder _builder;

        #endregion

        private ComponentManager() { }

        public ComponentManager InitializeComponents(Game game)
        {
            _builder = new ContainerBuilder();

            _builder.RegisterType<InputHandler>().WithParameter("game", game).SingleInstance();
            _builder.RegisterType<WorldComponent>().SingleInstance();

            return this;
        }

        public ComponentManager Register<T>(T @object) where T: class
        {
            _builder.RegisterInstance(@object).SingleInstance();

            return this;
        }

        public void Build()
        {
            _container = _builder.Build();
        }

        public TConcrete GetInstance<TConcrete>()
        {
            return _container.Resolve<TConcrete>();
        }
    }
}
