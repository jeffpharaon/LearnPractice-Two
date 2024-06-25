using System.Windows;
using System.Diagnostics;
using Repository;

namespace ProektSecond
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ссылка на класс с нестатическими объектами из dll
        private DataBase db = new DataBase();
        private Calculate cl = new Calculate();
        private AddData ad = new AddData();
        private DeleteData dd = new DeleteData();
        private EditData ed = new EditData();

        //переключатели для combobox
        private bool isKp70 = false;
        private bool isKp80 = false;
        private bool isKp100 = false;
        private bool isKpAll = false;

        //числовой результат калькулятора
        private int result = 0;

        //сохранение идентификатора рабочих
        private string oldId;
        //сохранение идентификатора ресурсов
        private string oldWhId;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSettings(); 
        }

        private void InitializeSettings() //обязательные первоначальные настройки
        {
            //настройки для проекта
            this.ResizeMode = ResizeMode.NoResize;
            HiddenElements();

            //настройки для БД
            db.Connect("JEFFPHARAON\\SQLEXPRESS", "Objects"); //указание имени сервера и БД
            LoadWorkesData();
            LoadWarehousesData();

            //добавление элементов выбора в ComboBox
            KpComboBox.Items.Add("KP70");
            KpComboBox.Items.Add("KP80");
            KpComboBox.Items.Add("KP100");
            KpComboBox.Items.Add("ВСЕ");
        }

        private void HiddenElements() //скрытие временно неиспользуемых элементов
        {
            WorkesGrid.Visibility = Visibility.Hidden;
            WarehousesGrid.Visibility = Visibility.Hidden;
            CalculateGrid.Visibility= Visibility.Hidden;
            AddGrid.Visibility= Visibility.Hidden;
            EditWorkesGrid.Visibility= Visibility.Hidden;
            AddWorkesGrid.Visibility= Visibility.Hidden;
            EditWorkesPanel.Visibility= Visibility.Hidden;
            EditWarehousesPanel.Visibility = Visibility.Hidden;
            EditGrid.Visibility = Visibility.Hidden;
            BtnExit.Visibility = Visibility.Hidden;
        }

        private void VisibleWorkes() //запуск окна с работниками
        {
            WorkesGrid.Visibility = Visibility.Visible;
            BtnExit.Visibility = Visibility.Visible;
        }

        private void VisibleWarehouses() //запуск окна со складами
        {
            WarehousesGrid.Visibility = Visibility.Visible;
            BtnExit.Visibility = Visibility.Visible;
        }

        private void VisibleCalculator() //запуск окна калькулятора
        {
            CalculateGrid.Visibility = Visibility.Visible;
            BtnExit.Visibility = Visibility.Visible;
        }

        private void LoadWorkesData() //метод загрузки данных о рабочих
        {
            db.AllInfoWorkes();
            foreach (string result in db.allInfoPeople)
                WorkesBox.Items.Add(result);

            db.Workes();
            foreach (string id in db.peopleId)
                WorkesEditBox.Items.Add(id);
            foreach (string name in db.peopleName)
                WorkesEditBox.Items.Add(name);  
            foreach (string departament in db.peopleDepartament)
                WorkesEditBox.Items.Add(departament);
            foreach (string post in db.peoplePost)
                WorkesEditBox.Items.Add(post);
        }

        private void LoadWarehousesData() //метод загрузки данных о складах
        {
            db.AllInfoWarehouses();
            foreach (string result in db.allInfoWh)
                WarehousesBox.Items.Add(result);

            db.Warehouses();
            foreach (string id in db.warehouseId)
                WarehousesEditBox.Items.Add(id);
            foreach (string kp70 in db.kp70)
                WarehousesEditBox.Items.Add(kp70);
            foreach (string kp80 in db.kp80)
                WarehousesEditBox.Items.Add(kp80);
            foreach (string kp100 in db.kp100)
                WarehousesEditBox.Items.Add(kp100);
        }

        private void CalcKp70() //метод реализации в UI подсчета калькулятора для ресурсов типа KP70
        { 
            cl.KP70(IdTextBox.Text);
            result = (int)cl.resultKp70;
            ResultLabel.Content = result.ToString() + " метр.";
        }

        private void CalcKp80() //метод реализации в UI подсчета калькулятора для ресурсов типа KP80
        {
            cl.KP80(IdTextBox.Text);
            result = (int)cl.resultKp80;
            ResultLabel.Content = result.ToString() + " метр.";
        }

        private void CalcKp100() //метод реализации в UI подсчета калькулятора для ресурсов типа KP100
        {
            cl.KP100(IdTextBox.Text);
            result = (int)cl.resultKp100;
            ResultLabel.Content = result.ToString() + " метр.";
        }

        private void CalcKpAll() //метод реализации в UI подсчета калькулятора для ресурсов всех типов
        {
            cl.AllKP(IdTextBox.Text);
            result = (int)cl.resultAll;
            ResultLabel.Content = result.ToString() + " метр.";
        }

        private void RestartUpdate()
        {
            //рестарт для обновления
            string currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(currentExecutablePath);
            Application.Current.Shutdown();
        }

        //кнопки реализации запуска окон
        private void BtnWorkes_Click(object sender, RoutedEventArgs e) => VisibleWorkes();

        private void BtnExit_Click(object sender, RoutedEventArgs e) => HiddenElements();

        private void BtnWarehouses_Click(object sender, RoutedEventArgs e) => VisibleWarehouses();

        private void BtnCalculate_Click(object sender, RoutedEventArgs e) => VisibleCalculator();

        //выбор типа рельс в combobox на калькуляторе 
        private void KpComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (KpComboBox.SelectedItem == null) return;

            string selectedItem = KpComboBox.SelectedItem.ToString();

            switch (selectedItem)
            {
                case "KP70":
                    isKp70 = true;
                    isKp80 = false;
                    isKp100 = false;
                    isKpAll = false;
                    break;

                case "KP80":
                    isKp70 = false;
                    isKp80 = true;
                    isKp100 = false;
                    isKpAll = false;
                    break;

                case "KP100":
                    isKp70 = false;
                    isKp80 = false;
                    isKp100 = true;
                    isKpAll = false;
                    break;

                case "ВСЕ":
                    isKp70 = false;
                    isKp80 = false;
                    isKp100 = false;
                    isKpAll = true;
                    break;
            }
        }

        //кнопка выполнения расчета калькулятора
        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (IdTextBox.Text == null || IdTextBox.Text == "")
                MessageBox.Show("Заполните поле идентификатора!", "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (isKp70 == true) CalcKp70();
            else if (isKp80 == true) CalcKp80();
            else if (isKp100 == true) CalcKp100();
            else if (isKpAll == true) CalcKpAll();

            else return;
        }

        //кнопка реализации добавления пользовательских данных в базу данных материалов
        private void SaveAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IdAddBox.Text == "" || Kp70AddBox.Text == "" || Kp80AddBox.Text == "" || Kp100AddBox.Text == "")
                MessageBox.Show("Заполните все поля!", "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                ad.AddMaterial(IdAddBox.Text, Kp70AddBox.Text, Kp80AddBox.Text, Kp100AddBox.Text);
                RestartUpdate();
            }
        }

        //кнопка открытия UI окна с добавлением пользовательских данных 
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
            => AddGrid.Visibility = Visibility.Visible;

        //удаление элемента из SQL таблицы (рабочие) по средством дабл-клика
        private void WorkesBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var choice = MessageBox.Show("Вы действительно хотите удалить?", "ВНИМАНИЕ",
      MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (choice == MessageBoxResult.Yes)
            {
                if (WorkesBox.SelectedItem != null)
                {
                    string selectedItem = WorkesBox.SelectedItem.ToString();
                    string[] parts = selectedItem.Split(',');
                    string nameToDelete = parts[0].Trim();

                    dd.DeleteWorkes(selectedItem, parts, nameToDelete);

                    WorkesBox.Items.Remove(WorkesBox.SelectedItem);

                    RestartUpdate();
                }
            }

            else return;
        }

        //отображение UI окна с добавлением пользовательских данных о рабочих
        private void BtnAddWorkes_Click(object sender, RoutedEventArgs e)
            => AddWorkesGrid.Visibility = Visibility.Visible;

        //отображение UI окна с редактированием пользовательских данных о рабочих
        private void BtnEdirWorkes_Click(object sender, RoutedEventArgs e)
            =>EditWorkesPanel.Visibility = Visibility.Visible;

        //добавление новых пользовательских данных рабочих в БД
        private void BtnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            ad.AddWorkes(idBoxAdd.Text, nameBoxAdd.Text, departamentBoxAdd.Text, postBoxAdd.Text);
            RestartUpdate();
        }

        //изменение пользователем данных рабочих в БД
        private void WorkesEditBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EditWorkesGrid.Visibility = Visibility.Visible;

            if (WorkesEditBox.SelectedItem != null)
            {
                string selectedItem = WorkesEditBox.SelectedItem.ToString();
                var workerData = ed.EditWorkes(selectedItem);

                if (workerData.Count > 0)
                {
                    idBox.Text = workerData["identify"];
                    nameBox.Text = workerData["name"];
                    departamentBox.Text = workerData["departament"];
                    postBox.Text = workerData["post"];

                    oldId = idBox.Text;
                }
            }

            else return;
        }

        //сохранение пользовательских изменений о рабочих в БД
        private void BtnSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            ed.SavedEditWorkes(idBox.Text, nameBox.Text, departamentBox.Text, postBox.Text, oldId);
            RestartUpdate();
        }

        //отображение UI окна с добавлением пользовательских данных складов и ресурсов
        private void BtnAddWarehouses_Click(object sender, RoutedEventArgs e)
            => AddGrid.Visibility = Visibility.Visible;

        //отображение UI окна с редактированием пользовательских данных складов и ресурсов
        private void BtnEditWarehouses_Click(object sender, RoutedEventArgs e)
            => EditWarehousesPanel.Visibility = Visibility.Visible;

        //удаление элемента из SQL таблицы (склады и ресурсы) по средством дабл-клика
        private void WarehousesBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var choice = MessageBox.Show("Вы действительно хотите удалить?", "ВНИМАНИЕ",
      MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (choice == MessageBoxResult.Yes)
            {
                if (WarehousesBox.SelectedItem != null)
                {
                    string selectedItem = WarehousesBox.SelectedItem.ToString();
                    string[] parts = selectedItem.Split(',');
                    string nameToDelete = parts[0].Trim();

                    dd.DeleteWarehouses(selectedItem, parts, nameToDelete);

                    WarehousesBox.Items.Remove(WarehousesBox.SelectedItem);

                    RestartUpdate();
                }
            }

            else return;
        }

        //изменение пользователем данных ресурсов и складов в БД
        private void WarehousesEditBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EditGrid.Visibility = Visibility.Visible;

            if (WarehousesEditBox.SelectedItem != null)
            {
                string selectedItem = WarehousesEditBox.SelectedItem.ToString();
                var workerData = ed.EditWarehouses(selectedItem);

                if (workerData.Count > 0)
                {
                    idEditBox.Text = workerData["identify"];
                    Kp70EditBox.Text = workerData["kp70"];
                    Kp80EditBox.Text = workerData["kp80"];
                    Kp100EditBox.Text = workerData["kp100"];

                    oldWhId = idEditBox.Text;
                }
            }

            else return;
        }

        //сохранение пользовательских изменений о складах и ресурсах в БД
        private void SaveEditBtn_Click(object sender, RoutedEventArgs e)
        {
            ed.SavedEditWarehouses(idEditBox.Text, Kp70EditBox.Text, Kp80EditBox.Text, Kp100EditBox.Text, oldWhId);
            RestartUpdate();
        }
    }
}
