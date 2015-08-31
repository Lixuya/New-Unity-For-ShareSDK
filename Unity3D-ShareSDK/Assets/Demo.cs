using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using cn.sharesdk.unity3d;

public class Demo : MonoBehaviour {

	private int reqID = 0;
	public GUISkin demoSkin;
	private ShareSDK ssdk;
	// Use this for initialization
	void Start ()
	{	
		ssdk = gameObject.GetComponent<ShareSDK>();
		ssdk.authHandler = AuthResultHandler;
		ssdk.shareHandler = ShareResultHandler;
		ssdk.showUserHandler = GetUserInfoResultHandler;
		ssdk.getFriendsHandler = GetFriendsResultHandler;
		ssdk.followFriendHandler = FollowFriendResultHandler;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
	
	void OnGUI ()
	{

		GUI.skin = demoSkin;
		
		float scale = 1.0f;

		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			scale = Screen.width / 320;
		}
		
		float btnWidth = 200 * scale;
		float btnHeight = 45 * scale;
		float btnTop = 20 * scale;
		GUI.skin.button.fontSize = Convert.ToInt32(16 * scale);
		
		reqID += 1;
		if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "Authorize"))
		{
			print(ssdk == null);
			ssdk.Authorize(reqID, PlatformType.SinaWeibo);
		}
		
		reqID += 1;
		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "Get User Info"))
		{
			ssdk.GetUserInfo(reqID, PlatformType.SinaWeibo);
		}
		
		reqID += 1;
		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "Show Share Menu"))
		{
			Hashtable content = new Hashtable();
			content["content"] = "this is a test string.";
			content["image"] = "http://img.baidu.com/img/image/zhenrenmeinv0207.jpg";
			content["title"] = "test title";
			content["description"] = "test description";
			content["url"] = "http://sharesdk.cn";
			content["type"] = ContentType.News;
			content["siteUrl"] = "http://sharesdk.cn";
			content["shareTheme"] = "classic";//ShareTheme has only two value which are skyblue and classic
			content["site"] = "ShareSDK";
			content["musicUrl"] = "http://mp3.mwap8.com/destdir/Music/2009/20090601/ZuiXuanMinZuFeng20090601119.mp3";
			//用sharesdk提供的onekeyshare库，有界面的快捷分享，包括九宫格和skybule风格
			ssdk.ShowShareMenu (reqID, null, content, 100, 100);
		}
		
		reqID += 1;
		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "Show Share View"))
		{
			Hashtable content = new Hashtable();
			content["content"] = "this is a test string.";
			content["image"] = "http://img.baidu.com/img/image/zhenrenmeinv0207.jpg";
			content["title"] = "test title";
			content["description"] = "test description";
			content["url"] = "http://sharesdk.cn";
			content["type"] = ContentType.News;
			content["siteUrl"] = "http://sharesdk.cn";
			content["shareTheme"] = "classic";//ShareTheme has only two value which are skyblue and classic
			content["site"] = "ShareSDK";
			content["musicUrl"] = "http://mp3.mwap8.com/destdir/Music/2009/20090601/ZuiXuanMinZuFeng20090601119.mp3";
			//用sharesdk提供的onekeyshare库，有界面的快捷分享，包括九宫格和skybule风格
			ssdk.ShowShareView (reqID, PlatformType.TencentWeibo, content);
		}
		
		reqID += 1;
		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "Share Content"))
		{
			Hashtable content = new Hashtable();
			content["content"] = "this is a test string.";
			content["image"] = "http://img.baidu.com/img/image/zhenrenmeinv0207.jpg";
			content["title"] = "test title";
			content["description"] = "test description";
			content["url"] = "http://sharesdk.cn";
			content["type"] = ContentType.News;
			content["siteUrl"] = "http://sharesdk.cn";
			content["site"] = "ShareSDK";
			content["musicUrl"] = "http://mp3.mwap8.com/destdir/Music/2009/20090601/ZuiXuanMinZuFeng20090601119.mp3";

			ssdk.ShareContent (reqID, PlatformType.SinaWeibo, content);
		}
		
		reqID += 1;
		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "Get Friends SinaWeibo "))
		{
			//获取新浪微博好友，第一页，每页15条数据
			print ("Click Btn Of Get Friends SinaWeibo");
			ssdk.GetFriendList (reqID, PlatformType.SinaWeibo, 15, 0);
		}

		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "Get Token SinaWeibo "))
		{
			Hashtable authInfo = ssdk.GetAuthInfo (PlatformType.SinaWeibo);			
			print ("share result :");
			print (MiniJSON.jsonEncode(authInfo));
		}

		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "Close SSO Auth"))
		{
			ssdk.CloseSSOWhenAuthorize (true);			
		}

		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "Remove Authorize "))
		{
			ssdk.CancelAuthorize (PlatformType.SinaWeibo);			
		}

		reqID += 1;
		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "Add Friend "))
		{
			//关注新浪微博
			ssdk.AddFriend (reqID, PlatformType.SinaWeibo, "3189087725");			
		}

	}
	
	void AuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
	{
		if (state == ResponseState.Success)
		{
			print ("authorize success !");
		}
		else if (state == ResponseState.Fail)
		{
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
		}
		else if (state == ResponseState.Cancel) 
		{
			print ("cancel !");
		}
	}
	
	void GetUserInfoResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable result)
	{
		if (state == ResponseState.Success)
		{
			print ("get user info result :");
			print (MiniJSON.jsonEncode(result));
		}
		else if (state == ResponseState.Fail)
		{
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
		}
		else if (state == ResponseState.Cancel) 
		{
			print ("cancel !");
		}
	}
	
	void ShareResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable result)
	{
		if (state == ResponseState.Success)
		{
			print ("share result :");
			print (MiniJSON.jsonEncode(result));
		}
		else if (state == ResponseState.Fail)
		{
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
		}
		else if (state == ResponseState.Cancel) 
		{
			print ("cancel !");
		}
	}

	void GetFriendsResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable result)
	{
		if (state == ResponseState.Success)
		{			
			print ("get friend list result :");
			print (MiniJSON.jsonEncode(result));
		}
		else if (state == ResponseState.Fail)
		{
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
		}
		else if (state == ResponseState.Cancel) 
		{
			print ("cancel !");
		}
	}

	void FollowFriendResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable result)
	{
		if (state == ResponseState.Success)
		{
			print ("Follow friend successfully !");
		}
		else if (state == ResponseState.Fail)
		{
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
		}
		else if (state == ResponseState.Cancel) 
		{
			print ("cancel !");
		}
	}
}
