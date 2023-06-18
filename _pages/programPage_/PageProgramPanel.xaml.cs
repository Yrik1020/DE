using Collage.src._pages.welcomePage_;
using Collage.src.DateBase;
using Collage.src.scripts;
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

namespace Collage.src._pages.programPage_
{
    /// <summary>
    /// Логика взаимодействия для PageProgramPanel.xaml
    /// </summary>
    public partial class PageProgramPanel : Page
    {
        private Entities entities = new Entities();
        private int[] classroom = new int[] { 40, 41, 42, 43, 44, 45, 46 };
        private string[] dateWeek = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
        private Random rnd = new Random();
        private int idClassRoom;
        private int couple_;
        private string date_;
        LoggingSystem loggingSystem = new LoggingSystem();
        public PageProgramPanel()
        {
            InitializeComponent();

            var schedule = entities.line_taskers.ToList();

            if (schedule != null)
            {
                lvScheduleMonday2ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Понедельник" && s.Professionals.named == "2 ИС-А").ToList();
                lvScheduleTuesday2ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Вторник" && s.Professionals.named == "2 ИС-А").ToList();
                lvScheduleWednesday2ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Среда" && s.Professionals.named == "2 ИС-А").ToList();
                lvScheduleThursday2ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Четверг" && s.Professionals.named == "2 ИС-А").ToList();
                lvScheduleFriday2ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Пятница" && s.Professionals.named == "2 ИС-А").ToList();
                lvScheduleSaturday2ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Суббота" && s.Professionals.named == "2 ИС-А").ToList();

                lvScheduleMonday2ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Понедельник" && s.Professionals.named == "2 ИС-Б").ToList();
                lvScheduleTuesday2ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Вторник" && s.Professionals.named == "2 ИС-Б").ToList();
                lvScheduleWednesday2ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Среда" && s.Professionals.named == "2 ИС-Б").ToList();
                lvScheduleThursday2ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Четверг" && s.Professionals.named == "2 ИС-Б").ToList();
                lvScheduleFriday2ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Пятница" && s.Professionals.named == "2 ИС-Б").ToList();
                lvScheduleSaturday2ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Суббота" && s.Professionals.named == "2 ИС-Б").ToList();

                lvScheduleMonday3ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Понедельник" && s.Professionals.named == "3 ИС-А").ToList();
                lvScheduleTuesday3ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Вторник" && s.Professionals.named == "3 ИС-А").ToList();
                lvScheduleWednesday3ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Среда" && s.Professionals.named == "3 ИС-А").ToList();
                lvScheduleThursday3ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Четверг" && s.Professionals.named == "3 ИС-А").ToList();
                lvScheduleFriday3ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Пятница" && s.Professionals.named == "3 ИС-А").ToList();
                lvScheduleSaturday3ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Суббота" && s.Professionals.named == "3 ИС-А").ToList();

                lvScheduleMonday3ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Понедельник" && s.Professionals.named == "3 ИС-Б").ToList();
                lvScheduleTuesday3ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Вторник" && s.Professionals.named == "3 ИС-Б").ToList();
                lvScheduleWednesday3ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Среда" && s.Professionals.named == "3 ИС-Б").ToList();
                lvScheduleThursday3ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Четверг" && s.Professionals.named == "3 ИС-Б").ToList();
                lvScheduleFriday3ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Пятница" && s.Professionals.named == "3 ИС-Б").ToList();
                lvScheduleSaturday3ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Суббота" && s.Professionals.named == "3 ИС-Б").ToList();

                lvScheduleMonday4ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Понедельник" && s.Professionals.named == "4 ИС-А").ToList();
                lvScheduleTuesday4ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Вторник" && s.Professionals.named == "4 ИС-А").ToList();
                lvScheduleWednesday4ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Среда" && s.Professionals.named == "4 ИС-А").ToList();
                lvScheduleThursday4ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Четверг" && s.Professionals.named == "4 ИС-А").ToList();
                lvScheduleFriday4ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Пятница" && s.Professionals.named == "4 ИС-А").ToList();
                lvScheduleSaturday4ISA.ItemsSource = entities.Line_schedule.Where(s => s.date == "Суббота" && s.Professionals.named == "4 ИС-А").ToList();

                lvScheduleMonday4ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Понедельник" && s.Professionals.named == "4 ИС-Б").ToList();
                lvScheduleTuesday4ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Вторник" && s.Professionals.named == "4 ИС-Б").ToList();
                lvScheduleWednesday4ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Среда" && s.Professionals.named == "4 ИС-Б").ToList();
                lvScheduleThursday4ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Четверг" && s.Professionals.named == "4 ИС-Б").ToList();
                lvScheduleFriday4ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Пятница" && s.Professionals.named == "4 ИС-Б").ToList();
                lvScheduleSaturday4ISB.ItemsSource = entities.Line_schedule.Where(s => s.date == "Суббота" && s.Professionals.named == "4 ИС-Б").ToList();

                btDeleteSchedule.IsEnabled = true;
            }
            else if(schedule.Count < 1)
            {
                btCreateSchedule.IsEnabled = true;
            }


        }

        private void btCreateSchedule_Click(object sender, RoutedEventArgs e)
        {
            int rowCountProf = entities.Professionals.Count();


            if (MessageBox.Show($"Вы точно использовать это дейсвтие?\nЭто может вызвать необратимые последствия!", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                for (int i = 0; i < rowCountProf + 1; i++)
                {
                    //var ProfList = entities.Professionals.Where(p => p.id_professional == i);

                    RandomGenericAllParams(i);
                    loggingSystem.LoggigModerUse("Генерация расписания");
                }

                // Исправляем дубликаты в расписании
                FixScheduleDuplicates();
                loggingSystem.LoggigModerUse("Исправление дублей расписания");

                for (int i = 0; i < rowCountProf + 1; i++)
                {
                    //var ProfList = entities.Professionals.Where(p => p.id_professional == i);

                    FixDuplicatePairsForGroup(i);
                }
                NavigationService.Navigate(new PageProgramPanel());
            }
        }
        private void FixDuplicatePairsForGroup(int idProf)
        {
            var lineSchedules = entities.Line_schedule
                .Where(ls => ls.Professionals.id_professional == idProf)
                .ToList();

            // Группировка расписания по дате и паре
            var duplicatePairs = lineSchedules
                .GroupBy(ls => new { ls.date, ls.couple })
                .Where(g => g.Count() > 1) // Выбираем только группы с дубликатами
                .ToList();

            foreach (var duplicatePair in duplicatePairs)
            {
                // Получаем все записи расписания для данной группы
                var schedules = duplicatePair.ToList();

                // Перебираем записи, начиная со второй
                for (int i = 1; i < schedules.Count; i++)
                {
                    // Генерируем новый случайный номер пары
                    int newCouple = (Convert.ToInt32(schedules[i - 1].couple) + 1) % 5; // Здесь 5 - количество пар в день

                    // Обновляем номер пары для записи
                    schedules[i].couple = newCouple;

                    // Сохраняем изменения в базе данных
                    entities.SaveChanges();
                }
            }
        }
        private void FixScheduleDuplicates()
        {
            var lineSchedules = entities.Line_schedule.ToList();

            // Группировка расписания по дате, времени и кабинету
            var duplicatePairs = lineSchedules
                .GroupBy(ls => new { ls.date, ls.couple, ls.classroom })
                .Where(g => g.Count() > 1) // Выбираем только группы с дубликатами
                .ToList();

            foreach (var duplicatePair in duplicatePairs)
            {
                // Получаем все записи расписания для данной группы
                var schedules = duplicatePair.ToList();

                // Перебираем записи, начиная со второй
                for (int i = 1; i < schedules.Count; i++)
                {
                    // Генерируем новый случайный кабинет
                    int randomClassroom = rnd.Next(0, classroom.Length);
                    int newClassroom = classroom[randomClassroom];

                    // Обновляем кабинет для записи
                    schedules[i].classroom = newClassroom;

                    // Сохраняем изменения в базе данных
                    entities.SaveChanges();
                }
            }
        }

        private void RandomGenericDayWeek(int idProf, int randomDay)
        {
            var lineSchedule_ = entities.Line_schedule.Where(lc => lc.id_prof == idProf).ToList();

            bool isDateUnique = false;
            while (!isDateUnique)
            {
                date_ = dateWeek[randomDay];
                isDateUnique = !lineSchedule_.Any(lc => lc.date == date_ && lc.id_prof != idProf);
            }
        }
        private void RandomGenericCouples(int idProf)
        {
            var lineSchedule_ = entities.Line_schedule.Where(lc => lc.id_prof == idProf).ToList();

            bool isCoupleUnique = false;
            while (!isCoupleUnique)
            {
                couple_ = rnd.Next(1, 4);
                isCoupleUnique = !lineSchedule_.Any(lc => lc.couple == couple_ && lc.id_prof != idProf);
            }
        }

        private void RandomGenericClassRoom(int idProf, int randomClassroom)
        {
            var lineSchedule_ = entities.Line_schedule.Where(lc => lc.id_prof == idProf).ToList();

            bool isClassRoomUnique = false;
            while (!isClassRoomUnique)
            {
                idClassRoom = classroom[randomClassroom];
                isClassRoomUnique = !lineSchedule_.Any(lc => lc.classroom == idClassRoom && lc.id_prof != idProf);
            }
        }

        private void RandomGenericAllParams(int idProf)
        {
            var lineProfDiscipline_ = entities.Line_Prof_Descipline.Where(lc => lc.id_prof == idProf);

            foreach (var lineProfDiscipline in lineProfDiscipline_)
            {
                double tempAllTimeWeek = Convert.ToInt32(lineProfDiscipline.all_time) / 40;
                tempAllTimeWeek = Math.Round(tempAllTimeWeek, MidpointRounding.ToEven);
                int idDiscipline = Convert.ToInt32(lineProfDiscipline.id_discipline);
                int randomDay = rnd.Next(0, 6);
                int randomClassroom = rnd.Next(0, 6);

                for (int i = 0; i < tempAllTimeWeek; i++)
                {
                    RandomGenericDayWeek(idProf, randomDay);
                    RandomGenericClassRoom(idProf, randomClassroom);
                    RandomGenericCouples(idProf);

                    Line_schedule line_Schedule = new Line_schedule();

                    line_Schedule.id_prof= idProf;
                    line_Schedule.date = date_;
                    line_Schedule.couple = couple_;
                    line_Schedule.classroom = idClassRoom;
                    line_Schedule.id_discipline = idDiscipline;

                    try
                    {
                        entities.Line_schedule.Add(line_Schedule);
                    }catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            try
            {
                entities.SaveChanges(); // Сохранение всех изменений в одной транзакции
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hlExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageSingIn());
        }

        private void hlEditProgram_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageEditProgramPanel());
        }

        private void btDeleteSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно использовать это дейсвтие?\nЭто может вызвать необратимые последствия!", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var deleteAllRow = entities.Line_schedule.ToList();

                    foreach(var items in deleteAllRow)
                    {
                        entities.Line_schedule.Remove(items);
                    }

                    entities.SaveChanges();
                    loggingSystem.LoggigModerUse("Удаление расписания");
                    NavigationService.Navigate(new PageProgramPanel());
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
