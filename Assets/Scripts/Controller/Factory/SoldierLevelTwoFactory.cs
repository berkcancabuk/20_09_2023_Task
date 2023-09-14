using Assets.Scripts.Model;
using Controller;

namespace Assets.Scripts.Controller.Factory
{
    public class SoldierLevelTwoFactory : IWarriorFactory
    {
        public IWarrior GenerateWarrior(Pointer3D position)
        {
            return new SoldierModel(2, 10, 5, position);
        }
    }
}