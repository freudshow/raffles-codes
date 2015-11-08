// (C) Copyright 2002-2005 by Autodesk, Inc. 
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to 
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//

//-----------------------------------------------------------------------------
//----- acrxEntryPoint.h
//-----------------------------------------------------------------------------
#include "StdAfx.h"
#include "resource.h"
#include <tchar.h>
#include <iostream>
#include "CreateEnt.h"
#include "ModifyEnt.h"
#include "Calculation.h"

using namespace std;
//-----------------------------------------------------------------------------
#define szRDS _RXST("YCRO")

//-----------------------------------------------------------------------------
//----- ObjectARX EntryPoint
class CYCRO_CGApp : public AcRxArxApp {

public:
	CYCRO_CGApp () : AcRxArxApp () {}

	virtual AcRx::AppRetCode On_kInitAppMsg (void *pkt) 
	{
		// TODO: Load dependencies here

		// You *must* call On_kInitAppMsg here
		AcRx::AppRetCode retCode =AcRxArxApp::On_kInitAppMsg (pkt) ;
		
		// TODO: Add your initialization code here

		return (retCode) ;
	}

	virtual AcRx::AppRetCode On_kUnloadAppMsg (void *pkt) 
	{
		// TODO: Add your code here

		// You *must* call On_kUnloadAppMsg here
		AcRx::AppRetCode retCode =AcRxArxApp::On_kUnloadAppMsg (pkt) ;

		// TODO: Unload dependencies here

		return (retCode) ;
	}

	virtual void RegisterServerComponents () 
	{
	}


	// - YCROYCRO_CG._ConvexHull command (do not rename)
	static void YCROYCRO_CG_ConvexHull(void)
	{
		// Add your code for command YCROYCRO_CG._ConvexHull here
		AcGePoint3d startPt(1.0, 1.0, 0.0);
		AcGePoint3d endPt(10.0, 10.0, 0.0);
		AcDbObjectId LineId = CCreateEnt::CreateLine(startPt,endPt);
		if (CModifyEnt::ChangeColor(LineId,3) == Acad::eOk)
		{
			acutPrintf(_T("\nConvexHull Done!\n"));
		}
	}
	static void YCROYCRO_CG_AddLine(void)
	{
		AcGePoint3d startPt(100.0, 100.0, 0.0);
		AcGePoint3d endPt(1088.0, 1099.0, 0.0);
		AcDbObjectId LineId = CCreateEnt::CreateLine(startPt,endPt);
		if (CModifyEnt::ChangeColor(LineId,3) == Acad::eOk)
		{
			acutPrintf(_T("\nCreate Line Done!\n"));
		}
	}
	static void YCROYCRO_CG_Hello(void)
	{
		acutPrintf(_T("\n+++++++++++++++++Hello World!+++++++++++++++++\n"));
	}

	static void YCROYCRO_CG_AddCircle()
	{
		AcGePoint3d ptCenter(100, 100, 0);
		CCreateEnt::CreateCircle(ptCenter, 20);
		AcGePoint2d pt1(70, 100);
		AcGePoint2d pt2(130, 100);
		CCreateEnt::CreateCircle(pt1, pt2);
		pt1.set(60, 100);
		pt2.set(140, 100);
		AcGePoint2d pt3(100, 60);
		CCreateEnt::CreateCircle(pt1, pt2, pt3);
	}

	static void YCROYCRO_CG_AddArc() 
	{ 
		AcGePoint2d ptCenter(50, 50); 
		CCreateEnt::CreateArc(ptCenter, 100 * sqrt(2.0) / 2, 5 * CCalculation::PI() / 4, 7 * CCalculation::PI() / 4); 
		AcGePoint2d ptStart(100, 0); 
		AcGePoint2d ptOnArc(120, 50); 
		AcGePoint2d ptEnd(100, 100); 
		CCreateEnt::CreateArc(ptStart, ptOnArc, ptEnd);
		ptStart.set(100, 100);
		ptCenter.set(50, 50);
		ptEnd.set(0, 100);
		CCreateEnt::CreateArcSCE(ptStart, ptCenter, ptEnd);
		ptStart.set(0, 100); 
		ptCenter.set(50, 50); 
		CCreateEnt::CreateArc(ptStart, ptCenter, CCalculation::PI() / 2); 
	}

	static void YCROYCRO_CG_AddPolyBulge()
	{
		AcGePoint2d pt1(400, 0);
		AcGePoint2d pt2(500, 100);
		AcGePoint2d pt3(600, 150);
		AcGePoint2d pt4(750, 200);
		AcGePoint2d pt5(800, 250);
		AcGePoint2d pt6(900, 300);
		AcGePoint2d pt7(950, 350);
		AcGePoint2d pt8(1000, 0);
		AcGePoint2dArray points;
		points.append(pt1);
		points.append(pt2);
		points.append(pt3);
		points.append(pt4);
		points.append(pt5);
		points.append(pt6);
		points.append(pt7);
		points.append(pt8);
		CModifyEnt::ChangeColor(CCreateEnt::CreatePolyline(points, 0.13, 2.0),3) ;
	}

	static void YCROYCRO_CG_AddPolyline() 
	{
		AcGePoint2d ptStart(0,0), ptEnd(100, 100); 
		CCreateEnt::CreatePolyline(ptStart, ptEnd, 1); 
		AcGePoint3d pt1(0, 0, 0), pt2(100, 0, 0), pt3(100, 100, 0); 
		AcGePoint3dArray points; 
		points.append(pt1); 
		points.append(pt2); 
		points.append(pt3); 
		CCreateEnt::Create3dPolyline(points);
		CCreateEnt::CreatePolygon(AcGePoint2d::kOrigin, 6, 30, 0, 1);
		AcGePoint2d pt(60, 70); 
		CCreateEnt::CreateRectangle(pt, ptEnd, 1);
		pt.set(50, 50); 
		CCreateEnt::CreatePolyCircle(pt, 30, 1);
		CCreateEnt::CreatePolyArc(pt, 50, CCalculation::GtoR(45), CCalculation::GtoR(225), 1); 
	}

	static void YCROYCRO_CG_AddEllipse() 
	{
		AcGeVector3d vecNormal(0, 0, 1); 
		AcGeVector3d majorAxis(40, 0, 0); 
		AcDbObjectId entId; 
		entId = CCreateEnt::CreateEllipse(AcGePoint3d::kOrigin, vecNormal, majorAxis, 0.5);
		AcGePoint2d pt1(60, 80), pt2(140, 120); 
		CCreateEnt::CreateEllipse(pt1, pt2); 
	}

	static void YCROYCRO_CG_AddSpline()
	{
		AcGePoint3d pt1(0, 0, 0), pt2(10, 30,0), pt3(60, 80, 0), pt4(100, 100, 
			0); 
		AcGePoint3dArray points; 
		points.append(pt1); 
		points.append(pt2); 
		points.append(pt3); 
		points.append(pt4); 
		CCreateEnt::CreateSpline(points);
		pt2.set(30, 10, 0); 
		pt3.set(80, 60, 0); 
		points.removeSubArray(0, 3); 
		points.append(pt1); 
		points.append(pt2); 
		points.append(pt3); 
		points.append(pt4); 
		AcGeVector3d startTangent(5, 1, 0); 
		AcGeVector3d endTangent(5, 1, 0); 
		CCreateEnt::CreateSpline(points, startTangent, endTangent); 
	}

	static void YCROYCRO_CG_AddRegion() 
	{
		ads_name ss; 
		int rt = acedSSGet(NULL, NULL, NULL, NULL, ss);
		AcDbObjectIdArray objIds; 
		if (rt == RTNORM) 
		{ 
			long length; 
			acedSSLength(ss, &length);
			for (int i = 0; i < length; i++) 
			{ 
				ads_name ent; 
				acedSSName(ss, i, ent); 
				AcDbObjectId objId; 
				acdbGetObjectId(objId, ent); 
				objIds.append(objId); 
			} 
		} 
		acedSSFree(ss);
		AcDbObjectIdArray regionIds;
		regionIds = CCreateEnt::CreateRegion(objIds); 
		int number = regionIds.length(); 
		if (number > 0) 
		{ 
			acutPrintf(_T("\n已经创建%d个面域！"), number); 
		} 
		else 
		{ 
			acutPrintf(_T("\n创建0个面域！")); 
		} 
	}

	static void YCROYCRO_CG_AddText() 
	{
		AcGePoint3d ptInsert(0, 4, 0); 
		CCreateEnt::CreateText(ptInsert, _T("CAD大观园"));
		ptInsert.set(0, 0, 0); 
		CCreateEnt::CreateMText(ptInsert, _T("http://www.cadhelp.net")); 
	}
};

//-----------------------------------------------------------------------------
IMPLEMENT_ARX_ENTRYPOINT(CYCRO_CGApp)

ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _ConvexHull, ConvexHull, ACRX_CMD_TRANSPARENT, NULL)
ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _Hello, Hello, ACRX_CMD_TRANSPARENT, NULL)
ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _AddLine, AddLine, ACRX_CMD_TRANSPARENT, NULL)
ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _AddCircle, AddCircle, ACRX_CMD_TRANSPARENT, NULL)
ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _AddArc, AddArc, ACRX_CMD_TRANSPARENT, NULL)
ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _AddPolyBulge, AddPolyBulge, ACRX_CMD_TRANSPARENT, NULL)
ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _AddPolyline, AddPolyline, ACRX_CMD_TRANSPARENT, NULL)
ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _AddEllipse, AddEllipse, ACRX_CMD_TRANSPARENT, NULL)
ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _AddSpline, AddSpline, ACRX_CMD_TRANSPARENT, NULL)
ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _AddRegion, AddRegion, ACRX_CMD_TRANSPARENT, NULL)
ACED_ARXCOMMAND_ENTRY_AUTO(CYCRO_CGApp, YCROYCRO_CG, _AddText, AddText, ACRX_CMD_TRANSPARENT, NULL)