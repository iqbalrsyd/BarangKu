using BarangKu.Models;
using BarangKu.Services;
using System.Windows.Input;

namespace BarangKu.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        private readonly OrderService _orderService;

        public ICommand TrackOrderCommand { get; }
        public ICommand RequestReturnCommand { get; }
        public ICommand CancelOrderCommand { get; }

        public OrderViewModel(Order order)
        {
            Order = order;
            _orderService = new OrderService();
            TrackOrderCommand = new RelayCommand(TrackOrder);
            RequestReturnCommand = new RelayCommand(RequestReturn);
            CancelOrderCommand = new RelayCommand(CancelOrder);
        }

        private void TrackOrder()
        {
            _orderService.TrackOrder(Order);
        }

        private void RequestReturn()
        {
            _orderService.RequestReturn(Order);
        }

        private void CancelOrder()
        {
            _orderService.CancelOrder(Order);
        }
    }
}
