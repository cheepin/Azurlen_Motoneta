using UnityEngine;
using System;

namespace util
{
	//パネルなどにある要素で満たすために使う。
	static public class FillElementUI
	{
		/// <summary>
		/// 横方向から埋めていく
		/// </summary>
		/// <param name="startPos">初期位置</param>
		/// <param name="elemInterval">エレメント（要素）の間隔</param>
		/// <param name="column">列数</param>
		/// <param name="row">行数</param>
		/// <param name="func">インスタンス化に使用 </param>
		static public void FillRows(Vector2 startPos,Vector2 elemInterval,int column,int row,Action<Vector2> func)
		{
			var positioner = startPos;
			for(int i = 0; i < column; i++)
			{
				for(int j = 0; j < row; j++)
				{
					func(positioner);
					positioner.x +=elemInterval.x;	
				}
				positioner.y +=elemInterval.y;	
				positioner.x = startPos.x;
			}
		}

		/// <summary>
		/// 縦方向から埋めていく
		/// </summary>
		/// <param name="startPos"> 初期位置  </param>
		/// <param name="elemInterval"> エレメント（要素）の間隔 </param>
		/// <param name="column"> 列数 </param>
		/// <param name="row"> 行数 </param>
		/// <param name="func"> インスタンス化に使用 func(現在位置) </param>
		static public void FillColumns(Vector2 startPos,Vector2 elemInterval,int column,int row,Action<Vector2> func)
		{
			var positioner = startPos;
			for(int i = 0; i < row; i++)
			{
				for(int j = 0; j < column; j++)
				{
					func(positioner);
					positioner.y +=elemInterval.y;	
				}
				positioner.x +=elemInterval.x;	
				positioner.y = startPos.y;
			}
		}

		/// <summary>
		/// 縦方向からを逆パターンで埋めていく
		/// </summary>
		/// <param name="startPos"> 初期位置  </param>
		/// <param name="elemInterval"> エレメント（要素）の間隔 </param>
		/// <param name="column"> 列数 </param>
		/// <param name="row"> 行数 </param>
		/// <param name="func"> インスタンス化に使用 func(現在位置) </param>
		static public void FillInverseColumns(Vector2 startPos,Vector2 elemInterval,int column,int row,Action<Vector2> func)
		{
			var positioner = startPos;
			for(int i = 0; i < row; i++)
			{
				for(int j = 0; j < column; j++)
				{
					func(positioner);
					positioner.y -=elemInterval.y;	
				}
				positioner.x +=elemInterval.x;	
				positioner.y = startPos.y;
			}
		}
	}

}