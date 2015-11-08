#pragma once
#include "StdAfx.h"
#include <dbents.h>
#include "Calculation.h"

class CModifyEnt
{
public:
	CModifyEnt(void);
	~CModifyEnt(void);
	static Acad::ErrorStatus ChangeColor(AcDbObjectId entId, Adesk::UInt16 colorIndex);
	static Acad::ErrorStatus ChangeLayer(AcDbObjectId entId, CString strLayerName);
	static Acad::ErrorStatus ChangeLinetype(AcDbObjectId entId, CString strLinetype);
	static Acad::ErrorStatus Rotate(AcDbObjectId entId, AcGePoint2d ptBase, double rotation);
	static Acad::ErrorStatus Move(AcDbObjectId entId, AcGePoint3d ptBase, AcGePoint3d ptDest);
	static Acad::ErrorStatus Scale(AcDbObjectId entId, AcGePoint3d ptBase, double scaleFactor); 
};