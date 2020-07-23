using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Magic.Actions
{
	public class CallMethodAction : TriggerAction<DependencyObject>
	{
		private class MethodDescriptor
		{
			public MethodInfo MethodInfo
			{
				get;
				private set;
			}

			public bool HasParameters
			{
				get
				{
					return this.Parameters.Length > 0;
				}
			}

			public int ParameterCount
			{
				get
				{
					return this.Parameters.Length;
				}
			}

			public ParameterInfo[] Parameters
			{
				get;
				private set;
			}

			public Type SecondParameterType
			{
				get
				{
					if (this.Parameters.Length >= 2)
					{
						return this.Parameters[1].ParameterType;
					}
					return null;
				}
			}

			public MethodDescriptor(MethodInfo methodInfo, ParameterInfo[] methodParams)
			{
				this.MethodInfo = methodInfo;
				this.Parameters = methodParams;
			}
		}

		private List<CallMethodAction.MethodDescriptor> methodDescriptors;

		public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register("TargetObject", typeof(object), typeof(CallMethodAction), new PropertyMetadata(new PropertyChangedCallback(CallMethodAction.OnTargetObjectChanged)));

		public static readonly DependencyProperty MethodNameProperty = DependencyProperty.Register("MethodName", typeof(string), typeof(CallMethodAction), new PropertyMetadata(new PropertyChangedCallback(CallMethodAction.OnMethodNameChanged)));

		public object TargetObject
		{
			get
			{
				return base.GetValue(CallMethodAction.TargetObjectProperty);
			}
			set
			{
				base.SetValue(CallMethodAction.TargetObjectProperty, value);
			}
		}

		public string MethodName
		{
			get
			{
				return (string)base.GetValue(CallMethodAction.MethodNameProperty);
			}
			set
			{
				base.SetValue(CallMethodAction.MethodNameProperty, value);
			}
		}

		private object Target
		{
			get
			{
				return this.TargetObject ?? base.AssociatedObject;
			}
		}

		public CallMethodAction()
		{
			this.methodDescriptors = new List<CallMethodAction.MethodDescriptor>();
		}

		protected override void Invoke(object parameter)
		{
			if (base.AssociatedObject != null)
			{
				CallMethodAction.MethodDescriptor methodDescriptor = this.FindBestMethod(parameter);
				if (methodDescriptor != null)
				{
					ParameterInfo[] parameters = methodDescriptor.Parameters;
					if (parameters.Length == 0)
					{
						methodDescriptor.MethodInfo.Invoke(this.Target, null);
						return;
					}
					if (parameters.Length == 2 && base.AssociatedObject != null && parameter != null && parameters[0].ParameterType.IsAssignableFrom(base.AssociatedObject.GetType()) && parameters[1].ParameterType.IsAssignableFrom(parameter.GetType()))
					{
						methodDescriptor.MethodInfo.Invoke(this.Target, new object[]
						{
							base.AssociatedObject,
							parameter
						});
						return;
					}
				}
				else if (this.TargetObject != null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Could not find method named '{0}' on object of type '{1}' that matches the expected signature.", new object[]
					{
						this.MethodName,
						this.TargetObject.GetType().Name
					}));
				}
			}
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			this.UpdateMethodInfo();
		}

		protected override void OnDetaching()
		{
			this.methodDescriptors.Clear();
			base.OnDetaching();
		}

		private CallMethodAction.MethodDescriptor FindBestMethod(object parameter)
		{
			if (parameter != null)
			{
				parameter.GetType();
			}
			return this.methodDescriptors.FirstOrDefault((CallMethodAction.MethodDescriptor methodDescriptor) => !methodDescriptor.HasParameters || (parameter != null && methodDescriptor.SecondParameterType.IsAssignableFrom(parameter.GetType())));
		}

		private void UpdateMethodInfo()
		{
			this.methodDescriptors.Clear();
			if (this.Target != null && !string.IsNullOrEmpty(this.MethodName))
			{
				Type type = this.Target.GetType();
				MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
				for (int i = 0; i < methods.Length; i++)
				{
					MethodInfo methodInfo = methods[i];
					if (this.IsMethodValid(methodInfo))
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (CallMethodAction.AreMethodParamsValid(parameters))
						{
							this.methodDescriptors.Add(new CallMethodAction.MethodDescriptor(methodInfo, parameters));
						}
					}
				}
				this.methodDescriptors = this.methodDescriptors.OrderByDescending(delegate (CallMethodAction.MethodDescriptor methodDescriptor)
				{
					int num = 0;
					if (methodDescriptor.HasParameters)
					{
						Type type2 = methodDescriptor.SecondParameterType;
						while (type2 != typeof(EventArgs))
						{
							num++;
							type2 = type2.BaseType;
						}
					}
					return methodDescriptor.ParameterCount + num;
				}).ToList<CallMethodAction.MethodDescriptor>();
			}
		}

		private bool IsMethodValid(MethodInfo method)
		{
			return string.Equals(method.Name, this.MethodName, StringComparison.Ordinal) && !(method.ReturnType != typeof(void));
		}

		private static bool AreMethodParamsValid(ParameterInfo[] methodParams)
		{
			if (methodParams.Length == 2)
			{
				if (methodParams[0].ParameterType != typeof(object))
				{
					return false;
				}
				if (!typeof(EventArgs).IsAssignableFrom(methodParams[1].ParameterType))
				{
					return false;
				}
			}
			else if (methodParams.Length != 0)
			{
				return false;
			}
			return true;
		}

		private static void OnMethodNameChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			CallMethodAction callMethodAction = (CallMethodAction)sender;
			callMethodAction.UpdateMethodInfo();
		}

		private static void OnTargetObjectChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			CallMethodAction callMethodAction = (CallMethodAction)sender;
			callMethodAction.UpdateMethodInfo();
		}
	}
}