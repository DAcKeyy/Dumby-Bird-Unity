using Zenject;

namespace DI.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            //SignalBusInstaller.Install(Container); TODO Глобальная для проекта шина
        }
    }
}