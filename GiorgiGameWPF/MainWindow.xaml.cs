using GiorgiGame.Enums;
using GiorgiGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GiorgiGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random _random;
        private int _totalRound = 1;
        private Player _user;
        private Player _ai;
        private BodyPart _bodyPart;
        public MainWindow()
        {
            InitializeComponent();
            Head.Tag = BodyPart.Head;
            Body.Tag = BodyPart.Body;
            Legs.Tag = BodyPart.Legs;
            HeadA.Tag = BodyPart.Head;
            BodyA.Tag = BodyPart.Body;
            LegsA.Tag = BodyPart.Legs;

            _random = new Random();
            Game(_totalRound);
        }

        private void Game(int round)
        {

            Head.IsChecked = true;
            _user = new Player(CurrentPlayer.User, round);
            _ai = new Player(CurrentPlayer.Ai, round);
            AiHealth.Text = $"Жизнь AI = {_ai.Health}";
            UserHealth.Text = $"Жизнь User = {_user.Health}";
            TotalRound.Text = $"Раунд = {_totalRound}";

            AttackButton.Visibility = Visibility.Visible;
            AtatckLabel.Visibility = Visibility.Visible;
            Head.Visibility = Visibility.Visible;
            Body.Visibility = Visibility.Visible;
            Legs.Visibility = Visibility.Visible;

            DefendButton.Visibility = Visibility.Collapsed;
            HeadA.Visibility = Visibility.Collapsed;
            BodyA.Visibility = Visibility.Collapsed;
            LegsA.Visibility = Visibility.Collapsed;
            DefendLabel.Visibility = Visibility.Collapsed;



        }
        private async void Defend_Clicked(object sender, RoutedEventArgs e)
        {

            AiHealth.Text = $"Жизнь AI = {_ai.Health}";
            UserHealth.Text = $"Жизнь User = {_user.Health}";
            _user.Defend(_bodyPart);
            Array values = Enum.GetValues(typeof(BodyPart));
            BodyPart randomBodyPart = (BodyPart)values.GetValue(_random.Next(values.Length));
            _ai.Attack(randomBodyPart, _user);
            AiResult.Text = $"AI Атоковал в {randomBodyPart}";

            if ( _user.Health < 0)
            {
                AiHealth.Text = $"Жизнь AI = {_ai.Health}";
                UserHealth.Text = $"Жизнь User = 0";
            }
            else
            {
                AiHealth.Text = $"Жизнь AI = {_ai.Health}";
                UserHealth.Text = $"Жизнь User = {_user.Health}";
            }
            


            if (_user.IsAlive == false || _user.Health <= 0)
            {
                AiResult.Text = $"Этот раунд выиграл AI";
                MessageBox.Show("Игра окончена вы проиграли");
                System.Windows.Application.Current.Shutdown();

            }

            DefendButton.Visibility = Visibility.Collapsed;
            HeadA.Visibility = Visibility.Collapsed;
            BodyA.Visibility = Visibility.Collapsed;
            LegsA.Visibility = Visibility.Collapsed;
            DefendLabel.Visibility = Visibility.Collapsed;

            AttackButton.Visibility = Visibility.Visible;
            AtatckLabel.Visibility = Visibility.Visible;
            Head.Visibility = Visibility.Visible;
            Body.Visibility = Visibility.Visible;
            Legs.Visibility = Visibility.Visible;




        }

        private async void AttackButton_Clicked(object sender, RoutedEventArgs e)
        {
            HeadA.IsChecked = true;
            AiHealth.Text = $"Жизнь AI = {_ai.Health}";
            UserHealth.Text = $"Жизнь User = {_user.Health}";

            Array values = Enum.GetValues(typeof(BodyPart));
            BodyPart randomBodyPart = (BodyPart)values.GetValue(_random.Next(values.Length));
            _ai.Defend(randomBodyPart);
            _user.Attack(_bodyPart, _ai);

            AiResult.Text = $"AI Защищался в {randomBodyPart}";

            if (_ai.Health < 0)
            {
                AiHealth.Text = $"Жизнь AI = 0";
                UserHealth.Text = $"Жизнь User = {_user.Health}";
            }
            else
            {
                AiHealth.Text = $"Жизнь AI = {_ai.Health}";
                UserHealth.Text = $"Жизнь User = {_user.Health}";
            }


            if (_ai.IsAlive == false || _ai.Health <= 0)
            {
                AiResult.Text = $"Прошлый раунд выиграл User";
                ++_totalRound;

                if (_totalRound == 4)
                {
                    MessageBox.Show("Игра окончена вы выиграли");
                    System.Windows.Application.Current.Shutdown();
                }

                
                Game(_totalRound);


            }
            else
            {

                DefendButton.Visibility = Visibility.Visible;
                HeadA.Visibility = Visibility.Visible;
                BodyA.Visibility = Visibility.Visible;
                LegsA.Visibility = Visibility.Visible;
                DefendLabel.Visibility = Visibility.Visible;


                AttackButton.Visibility = Visibility.Collapsed;
                AtatckLabel.Visibility = Visibility.Collapsed;
                Head.Visibility = Visibility.Collapsed;
                Body.Visibility = Visibility.Collapsed;
                Legs.Visibility = Visibility.Collapsed;

            }



        }

        private void Attack_CheckedChanged(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRadioButton = ((RadioButton)sender);
            _bodyPart = (BodyPart)selectedRadioButton.Tag;
        }

       
    }
}
