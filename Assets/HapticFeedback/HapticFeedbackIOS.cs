using UnityEngine; 
using System.Runtime.InteropServices;

public class HapticFeedbackIOS {
	
	[DllImport ("__Internal")] private static extern void _initSelectionFeedback ();
	[DllImport ("__Internal")] private static extern void _prepareSelectionFeedback ();
	[DllImport ("__Internal")] private static extern void _selectionFeedback ();

	[DllImport ("__Internal")] private static extern void _initImpactFeedback (int strength);
	[DllImport ("__Internal")] private static extern void _prepareImpactFeedback (int strength);
	[DllImport ("__Internal")] private static extern void _impactFeedback (int strength);

	[DllImport ("__Internal")] private static extern void _initNotificationFeedback ();
	[DllImport ("__Internal")] private static extern void _prepareNotificationFeedback ();
	[DllImport ("__Internal")] private static extern void _notificationFeedbackSuccess ();
	[DllImport ("__Internal")] private static extern void _notificationFeedbackError ();
	[DllImport ("__Internal")] private static extern void _notificationFeedbackWarning ();


	public static void InitSelectionFeedback ()
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_initSelectionFeedback();
		}
	}

	public static void PrepareSelectionFeedback ()
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_prepareSelectionFeedback();
		}
	}

	public static void SelectionFeedback ()
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_selectionFeedback();
		}
	}
	
	
	

	public static void InitImpactFeedback (int strength)
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_initImpactFeedback(strength);
		}
	}

	public static void PrepareImpactFeedback (int strength)
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_prepareImpactFeedback(strength);
		}
	}

	public static void ImpactFeedback (int strength)
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_impactFeedback(strength);
			Debug.Log("THIS HAPPENED");
		}
	}

	


	public static void InitNotificationFeedback ()
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_initNotificationFeedback();
		}
	}

	public static void PrepareNotificationFeedback ()
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_prepareNotificationFeedback();
		}
	}

	public static void NotificationFeedbackSuccess ()
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_notificationFeedbackSuccess();
		}
	}
	
	public static void NotificationFeedbackError ()
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer )
		{
			_notificationFeedbackError();
		}
	}

	public static void NotificationFeedbackWarning ()
	{
		if ( Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_notificationFeedbackWarning();
		}
	}


}