using Assets.Scripts.Model;
using Controller;

namespace Assets.Scripts.Controller.Factory
{
    public class SoldierLevelOneFactory : IWarriorFactory
    {
        public IWarrior GenerateWarrior(Pointer3D position)
        {
            return new SoldierModel(1, 10, 2, position);
        }
    }
}