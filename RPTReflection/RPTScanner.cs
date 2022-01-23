using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPTReflection {
	class RPTScanner {
		private bool processRunning;
		public float UpdateFrequency {
			get { return UpdateFrequency; }
			set { UpdateFrequency = value; }
		}
		public int MyProperty {
			get { return MyProperty; }
			set { MyProperty = value; }
		}




		public RPTScanner() {
			processRunning = false;
			UpdateFrequency = 0;

		}

		void Start() {
			if (processRunning) {
				throw new InvalidOperationException("The process had already started!");
			}
			processRunning = true;
		}
	}
}
