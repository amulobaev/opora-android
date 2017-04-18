using System;
using System.Collections.Generic;
using System.Text;

using Opora.Models;

namespace Opora.ViewModels
{
    /// <summary>
    /// Модель представления создания/изменения опоры
    /// </summary>
    public class EditPillarViewModel : BaseViewModel
    {
        public EditPillarViewModel(Pillar item = null)
        {
            Title = "Опора";
        }
    }
}