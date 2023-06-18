using Collage.src.DateBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Collage.src.scripts
{
    public class LoggingSystem
    {
        private int UserID = Convert.ToInt32(Application.Current.Properties["UserID"]);
        private Entities entities = new Entities();

        private void GeneralLogginSystem(string typeLog, string discription) 
        {
            Logging logging = new Logging();

            DateTime dateTime = DateTime.Now;

            if (discription.Contains("Система -"))
                logging.id_user = null;
            else
                logging.id_user = UserID;

            logging.datetime = dateTime;
            logging.discription = discription;
            logging.type_log = typeLog;
            entities.Logging.Add(logging);

            try
            {
                entities.SaveChanges();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LogginUserUse(string discription)
        {
            var user = entities.Users.FirstOrDefault(u => u.id_user == UserID);
            string typeUser = user.User_types.named;
            string typeLog = "Пользовательские действия";

            string discription_ = $"{typeUser} - {discription}";

            GeneralLogginSystem(typeLog, discription_);
        }
        public void LogginAdminUse(string discription)
        {
            var user = entities.Users.FirstOrDefault(u => u.id_user == UserID);
            string typeUser = user.User_types.named;
            string typeLog = "Административные действия";

            string discription_ = $"{typeUser} - {discription}";

            GeneralLogginSystem(typeLog, discription_);
        }
        public void LoggigModerUse(string discription)
        {
            var user = entities.Users.FirstOrDefault(u => u.id_user == UserID);
            string typeUser = user.User_types.named;
            string typeLog = "Модераторские действия";

            string discription_ = $"{typeUser} - {discription}";

            GeneralLogginSystem(typeLog, discription_);
        }
        public void LogginSystem(string discription)
        {
            string typeLog = "Системное действия";

            string discription_ = $"Система - {discription}";

            GeneralLogginSystem(typeLog, discription_);
        }
    }
}
