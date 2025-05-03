using WinUI.Repository;
using WinUI.Service;

namespace WinUI.View
{
    using WinUI.Model;
    using WinUI.ViewModel;
    using Microsoft.UI.Xaml.Controls;

    /// <summary>
    /// The view class for recommendation system.
    /// </summary>
    public sealed partial class RecommendationView : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationView"/> class.
        /// </summary>
        public RecommendationView()
        {
            var doctorDbService = new DoctorRepository();
            DoctorService doctorManager = new DoctorService(doctorDbService);
            RecommendationSystemModel recommendationSystem = new RecommendationSystemModel(doctorManager);


            this.DataContext = new RecommendationSystemFormViewModel(recommendationSystem);
            this.InitializeComponent();
        }
    }
}
