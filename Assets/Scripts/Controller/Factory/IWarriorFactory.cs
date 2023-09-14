using Assets.Scripts.Model;
using Controller;

namespace Assets.Scripts.Controller.Factory
{
    public interface IWarriorFactory
    {
        public IWarrior GenerateWarrior(Pointer3D position);
    }
}