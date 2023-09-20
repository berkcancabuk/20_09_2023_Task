using Assets.Scripts.Model;

namespace Model.Interface
{
    public interface ISoldierBarracks: IBuilding, IClickable
    {
        public bool IsInstantiate { get; set; }
    }

   
}