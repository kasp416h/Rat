using BLL.Repository;
using DLL;
using DLL.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IRepository
    {
        public SaveCenter LoadData();
        public void SendData(RaceManager raceManager, List<Bet> bet);
    }
}
