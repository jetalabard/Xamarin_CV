package md5014bda19d00f3bd5730042e07a0083ae;


public class MyFragmentPagerAdapter
	extends android.support.v13.app.FragmentPagerAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getItem:(I)Landroid/app/Fragment;:GetGetItem_IHandler\n" +
			"n_getPageTitle:(I)Ljava/lang/CharSequence;:GetGetPageTitle_IHandler\n" +
			"n_getCount:()I:GetGetCountHandler\n" +
			"";
		mono.android.Runtime.register ("TalabardJeremyCv.XView.Fragment.MyFragmentPagerAdapter, TalabardJeremyCv", MyFragmentPagerAdapter.class, __md_methods);
	}


	public MyFragmentPagerAdapter (android.app.FragmentManager p0)
	{
		super (p0);
		if (getClass () == MyFragmentPagerAdapter.class)
			mono.android.TypeManager.Activate ("TalabardJeremyCv.XView.Fragment.MyFragmentPagerAdapter, TalabardJeremyCv", "Android.App.FragmentManager, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public android.app.Fragment getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native android.app.Fragment n_getItem (int p0);


	public java.lang.CharSequence getPageTitle (int p0)
	{
		return n_getPageTitle (p0);
	}

	private native java.lang.CharSequence n_getPageTitle (int p0);


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
