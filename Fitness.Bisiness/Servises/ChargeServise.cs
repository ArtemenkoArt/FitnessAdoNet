using Fitness.AdoNet.Repositories;
using Fitness.DataObject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Bisiness.Servises
{
    public interface IChargeServise
    {
        IEnumerable<ChargeExtendet> GetList();
        ChargeExtendet GetItem(int id);
        void Insert(ChargeExtendet chargeExtendet);
        void Update(ChargeExtendet chargeExtendet);
        void Delete(int idCharge);
    }

    public class ChargeServise : IChargeServise
    {
        private readonly IChargeRepository _chargeRepository;

        public ChargeServise(string stringConnection)
        {
            _chargeRepository = new ChargeRepository(stringConnection);
        }

        public ChargeExtendet GetItem(int id)
        {
            return _chargeRepository.GetItem(id);
        }

        public IEnumerable<ChargeExtendet> GetList()
        {
            return _chargeRepository.GetList();
        }

        public void Insert(ChargeExtendet chargeExtendet)
        {
            _chargeRepository.Insert(chargeExtendet);
        }

        public void Update(ChargeExtendet chargeExtendet)
        {
            _chargeRepository.Update(chargeExtendet);
        }

        public void Delete(int idCharge)
        {
            _chargeRepository.Delete(idCharge);
        }
    }
}
