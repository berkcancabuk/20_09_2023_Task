using Assets.Scripts.Model;
using Controller;

namespace Assets.Scripts.Controller.Factory
{
    public class SoldierLevelThreeFactory : IWarriorFactory
    {
        public IWarrior GenerateWarrior(Pointer3D position)
        {
            return new SoldierModel(3, 10, 10, position);
        }
    }
}