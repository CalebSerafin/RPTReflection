using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPTReflection {
	static class Log {
#if !DEBUG
		public static void Verbose(string message) {}
		public static void Info(string message) {}
		public static void Warning(string message) {}
		public static void Error(string message) {}
#else
		[Conditional ("DEBUG")]
		public static void Verbose(string message) {

		}
#endif
	}
}
