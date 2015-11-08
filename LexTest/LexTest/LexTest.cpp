// LexTest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include<stdio.h>
#include<string.h>
#include<iostream>
using namespace std;

char prog[80],token[8];
char ch;
int syn,p,m=0,n,row,sum=0;
char *rwtab[6]={"begin","if","then","while","do","end"};

void scaner()
{
    /*
        共分为三大块，分别是标示符、数字、符号，对应下面的 if   else if  和 else 
    */ 
    for(n=0;n<8;n++) token[n]=NULL;
    ch=prog[p++];
    while(ch==' ')
    {
        ch=prog[p];
        p++;
    }
    if((ch>='a'&&ch<='z')||(ch>='A'&&ch<='Z'))  //可能是标示符或者变量名 
    {
        m=0;
        while((ch>='0'&&ch<='9')||(ch>='a'&&ch<='z')||(ch>='A'&&ch<='Z'))
        {
            token[m++]=ch;
            ch=prog[p++];
        }
        token[m++]='\0';
        p--;
        syn=10;
        for(n=0;n<6;n++)  //将识别出来的字符和已定义的标示符作比较， 
            if(strcmp(token,rwtab[n])==0)
            {
                syn=n+1;
                break;
            }
    }
    else if((ch>='0'&&ch<='9'))  //数字 
    {
        {
            sum=0;
            while((ch>='0'&&ch<='9'))
            {
                sum=sum*10+ch-'0';
                ch=prog[p++];
            }
        }
        p--;
        syn=11;
        if(sum>32767)
            syn=-1;
    }
    else switch(ch)   //其他字符 
    {
        case'<':m=0;token[m++]=ch;
            ch=prog[p++];
            if(ch=='>')
            {
                syn=21;
                token[m++]=ch;
            }
            else if(ch=='=')
            {
                syn=22;
                token[m++]=ch;
            }
            else
            {
                syn=23;
                p--;
            }
            break;
        case'>':m=0;token[m++]=ch;
            ch=prog[p++];
            if(ch=='=')
            {
                syn=24;
                token[m++]=ch;
            }
            else
            {
                syn=20;
                p--;
            }
            break;
        case':':m=0;token[m++]=ch;
            ch=prog[p++];
            if(ch=='=')
            {
                syn=18;
                token[m++]=ch;
            }
            else
            {
                syn=17;
                p--;
            }
            break;
        case'*':syn=13;token[0]=ch;break;
        case'/':syn=14;token[0]=ch;break;
        case'+':syn=15;token[0]=ch;break;
        case'-':syn=16;token[0]=ch;break;
        case'=':syn=25;token[0]=ch;break;
        case';':syn=26;token[0]=ch;break;
        case'(':syn=27;token[0]=ch;break;
        case')':syn=28;token[0]=ch;break;
        case'#':syn=0;token[0]=ch;break;
        case'\n':syn=-2;break;
        default: syn=-1;break;
    }
}

int main()
{
    p=0;
    row=1;
    cout<<"Please input string:"<<endl;
    do
    {
        cin.get(ch);
        prog[p++]=ch;
    }
    while(ch!='#');
    p=0;
    do
    {
        scaner();
        switch(syn)
        {
        case 11: cout<<"("<<syn<<","<<sum<<")"<<endl; break;  
        case -1: cout<<"Error in row "<<row<<"!"<<endl; break;
        case -2: row=row++;break;
        default: cout<<"("<<syn<<","<<token<<")"<<endl;break;
        }
    }
    while (syn!=0);
	system("PAUSE");
}