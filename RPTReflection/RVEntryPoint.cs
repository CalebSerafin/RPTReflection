using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using RGiesecke.DllExport;
using System.Threading.Tasks;

namespace RPTReflection {
	public class RVEntryPoint {
		public delegate int ExtensionCallback([MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.LPStr)] string function, [MarshalAs(UnmanagedType.LPStr)] string data);
		public static ExtensionCallback callback;
		private static int numberOfCalls = 0;

#if WIN64
		[DllExport("RVExtensionRegisterCallback", CallingConvention = CallingConvention.Winapi)]
#else
		[DllExport("_RVExtensionRegisterCallback@4", CallingConvention = CallingConvention.Winapi)]
#endif
		public static void RVExtensionRegisterCallback([MarshalAs(UnmanagedType.FunctionPtr)] ExtensionCallback func) {
			callback = func;
		}

#if WIN64
		[DllExport("RVExtensionVersion", CallingConvention = CallingConvention.Winapi)]
#else
		[DllExport("_RVExtensionVersion@8", CallingConvention = CallingConvention.Winapi)]
#endif
		public static void RvExtensionVersion(StringBuilder output, int outputSize) {
			output.Append("RPTReflection v0.1");
		}

#if WIN64
		[DllExport("RVExtension", CallingConvention = CallingConvention.Winapi)]
#else
		[DllExport("_RVExtension@12", CallingConvention = CallingConvention.Winapi)]
#endif
		public static void RvExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function) {
			int callNumber = Interlocked.Increment(ref numberOfCalls);
			output.Append($"function: {function}. This was call number {callNumber:D}.");
			_ = Task.Run(async () => {
				await Task.Delay(1000);
				callback.Invoke($"name: {nameof(RPTReflection)}", $"function: {function}", $"Callback No: {callNumber:D}");
			});
		}

#if WIN64
		[DllExport("RVExtensionArgs", CallingConvention = CallingConvention.Winapi)]
#else
		[DllExport("_RVExtensionArgs@20", CallingConvention = CallingConvention.Winapi)]
#endif
		public static int RvExtensionArgs(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] args, int argCount) {
			output.Append(string.Join(", ", args));
            return 0;
        }
    }
}