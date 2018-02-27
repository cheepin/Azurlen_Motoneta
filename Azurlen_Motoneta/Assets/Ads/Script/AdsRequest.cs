using GoogleMobileAds.Api;
using util;
using System;
namespace ads
{
	public class AdsRequest : Singleton<AdsRequest>
	{
		InterstitialAd interstitial;
		int counter;
		static public void RequestBanner()
		{
			 #if UNITY_EDITOR
			string adUnitId = "unused";
			#elif UNITY_ANDROID
				string adUnitId = "ca-app-pub-5502459139425740/3628106367";
			#endif

			// Create a 320x50 banner at the top of the screen.
			BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder().Build();
			// Load the banner with the request.
			bannerView.LoadAd(request);
		}

		/// <summary>
		/// インタースティシャル広告を表示する
		/// </summary>
		/// <param name="callBackAfterAds">広告を表示した後、もしくは広告表示に失敗した後に実行するデリゲート</param>
		static public void Show(Action callBackAfterAds)
		{
			
			Instance.callBack = callBackAfterAds;
			if(Instance.interstitial.IsLoaded() && (++Instance.counter%6) ==0)
			{
				print("IsReady");
				Instance.interstitial.Show();
				Instance.interstitial.OnAdClosed += Interstitial_OnAdClosed;
				Instance.interstitial.OnAdFailedToLoad += Interstitial_OnAdFailed;
			}
			else
			{
				callBackAfterAds.Invoke();
			}

		}

		Action callBack = null;
		bool callFlag=false;
		private static void Interstitial_OnAdClosed(object sender, EventArgs e)
		{
			print("invoke");
			Instance.callFlag=true;
			Instance.interstitial.OnAdClosed -= Interstitial_OnAdClosed;
		}

		bool failFlag = false;
		private static void Interstitial_OnAdFailed(object sender, EventArgs e)
		{
			print("failed");
			Instance.failFlag = true;
			Instance.interstitial.OnAdFailedToLoad -= Interstitial_OnAdFailed;

		}

		private void LateUpdate()
		{
			if(Instance.callFlag)
			{
				Instance.callBack?.Invoke();
				Instance.callBack = null;
				Instance.callFlag = false;
				Instance.counter = 1;
				LoadAds();
			}
			//---広告の読み込みに失敗した場合
			//---カウンターを一つ減らす
			else if(Instance.failFlag)
			{
				Instance.callBack?.Invoke();
				--Instance.counter;
				Instance.callBack = null;

			}
		}

		public void HandleAdFailedToLoad(EventArgs args)
		{
		}

		void Start()
		{
			LoadAds();
			Instance.counter = 1;
		}

		static void LoadAds()
		{
			#if UNITY_ANDROID
				string adUnitId = "ca-app-pub-5502459139425740/3628106367";
			#elif UNITY_IPHONE
				string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
			#else
				string adUnitId = "unexpected_platform";
			#endif

			// Initialize an InterstitialAd.
			Instance.interstitial = new InterstitialAd(adUnitId);
			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder().Build();
			// Load the interstitial with the request.
			Instance.interstitial.LoadAd(request);
		}


	} 
}
