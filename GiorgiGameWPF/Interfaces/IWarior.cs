using GiorgiGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiorgiGame.Interfaces
{
    public interface IWarior
    {
        CurrentPlayer CurrentPlayer { get; }
        int Health { get; }
        BodyPart AttackBodypart { get; set; }
        BodyPart DefendBodyPart { get; }
        bool IsAlive { get; }
        void Attack(BodyPart attackBodypart, IWarior oponentWarior);
        void Defend(BodyPart defendBodypart);
    }
}
