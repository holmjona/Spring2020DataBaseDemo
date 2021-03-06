﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseDemo {
    class SuperHero {
        #region Private Variables
        private int _ID;
		private string _FirstName;
		private string _LastName;
		private DateTime _DateOfBirth;
		private decimal _Height;
		private int _AlterEgoID;
		private Citizen _AlterEgo = null;
		private List<SuperHero> _Friends = null;
        #endregion

        #region Public Properties
        public int ID {
			get { return _ID; }
			set { _ID = value; }
		}


		public string FirstName {
			get { return _FirstName; }
			set { _FirstName = value; }
		}


		public string LastName {
			get { return _LastName; }
			set { _LastName = value; }
		}


		public DateTime DateOfBirth {
			get { return _DateOfBirth; }
			set { _DateOfBirth = value; }
		}


		public decimal Height {
			get { return _Height; }
			set { _Height = value; }
		}

		public int AlterEgoID {
			get { return _AlterEgoID; }
			set { _AlterEgoID = value; }
		}

		public Citizen AlterEgo {
			get {
				if(_AlterEgo == null) {
					_AlterEgo = DAL.GetCitizen(_AlterEgoID);
				}
				return _AlterEgo;
			}
			set {
				_AlterEgo = value;
				if(value != null) {
					_AlterEgoID = value.ID;
				} else {
					_AlterEgoID = -1;
				}
			}
		}

		public List<SuperHero> Friends {
			get {
				if (_Friends == null) {
					_Friends = DAL.GetSuperFriends(this);
				}
				return _Friends;
			}
		}



		#endregion

		public override string ToString() {
			return FirstName + " " + LastName;
		}

	}
}
