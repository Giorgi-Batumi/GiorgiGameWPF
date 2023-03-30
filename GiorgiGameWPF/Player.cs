using GiorgiGame.Enums;
using GiorgiGame.Interfaces;
using System.Threading.Tasks;

namespace GiorgiGame
{
    public class Player : IWarior
    {
        private BodyPart _attackBodypart;

        private int _currentRound;

        public Player(CurrentPlayer currentPlayer, int currentRound)
        {
            CurrentPlayer = currentPlayer;
            _currentRound = currentRound;
            SetHealth();
            IfAlive();

        }
        public CurrentPlayer CurrentPlayer { get; private set; }
        public int Health { get; private set; } = 30;
        public BodyPart AttackBodypart
        {
            get
            {
                return _attackBodypart;
            }
            set
            {
                _attackBodypart = value;
                AttackResult();
            }
        }
        public BodyPart DefendBodyPart { get; private set; }
        public bool IsAlive { get; private set; } = true;


        public void Attack(BodyPart attackBodypart, IWarior oponentWarior)
        {
            oponentWarior.AttackBodypart = attackBodypart;

        }


        public void Defend(BodyPart defendBodypart)
        {
            DefendBodyPart = defendBodypart;

        }
        private void IfAlive()
        {
            Task.Run(() =>
            {

                do
                {
                    if (Health == 0)
                    {
                        break;
                    }
                } while (Health > 0);

                IsAlive = false;

            });

        }
        private void SetHealth()
        {
            if (CurrentPlayer == CurrentPlayer.Ai && _currentRound == 2)
            {
                Health = 35;
            }
            if (CurrentPlayer == CurrentPlayer.Ai && _currentRound == 3)
            {
                Health = 40;
            }
        }
        private void AttackResult()
        {
            if (DefendBodyPart == AttackBodypart)
            {
                Health -= 1;
            }
            else
            {
                Health -= 5;
            }

        }
    }
}
