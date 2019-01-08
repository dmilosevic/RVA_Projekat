using Client.Proxy;
using Client.ViewModel;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands
{
    class AddMeasurement : BaseCommand
    {
        AddMeasurementVM viewModel = null;

        public AddMeasurement(AddMeasurementVM vm)
        {
            viewModel = vm;
        }

        public override void Execute(object parameter)
        {
            if (viewModel.Type == null || viewModel.Type == "")
                return;
            if (viewModel.Value == null || viewModel.Value == "")
                return;
            if (viewModel.Unit == null || viewModel.Unit == "")
                return;
            if (viewModel.SelectedDate == null)
                return;
            if (viewModel.SelectedDev == null || viewModel.SelectedDev.Id == "")
                return;
            

            Measurement newMeas = new Measurement()
            {
                Type = viewModel.Type,
                Unit = viewModel.Unit,
                Value = float.Parse(viewModel.Value),
                 DateTime = viewModel.SelectedDate,
                  Measurement_Device = viewModel.SelectedDev.Id,
            };

           bool success = DataProxy.Instance.Proxy.AddMeasurement(newMeas);

            if (!success)
            {
                LoginVM.Log.Error($"Tried to add duplicate Measurement. Id=('{newMeas.Id}')");
                return;
            }
            else
            {
                LoginVM.Log.Info($"Measurement added successfuly. Id=('{newMeas.Id}')");

                viewModel.homeVM.RefreshData();
                viewModel.view.Close();
            }
        }
    }
}
