
var W = screen.width;//取得屏幕分辨率宽度
        var H = screen.height;//取得屏幕分辨率高度
        var x;
        var y;
        function mouseMove(ev)
        {
         ev= ev || window.event;
          var mousePos = mouseCoords(ev);
          //alert(ev.pageX);
              x = mousePos.x;
              y = mousePos.y;
        }

        function mouseCoords(ev)
        {
         if(ev.pageX || ev.pageY){
           return {x:ev.pageX, y:ev.pageY};
         }
         return {
             x:ev.clientX + document.body.scrollLeft - document.body.clientLeft,
             y:ev.clientY + document.body.scrollTop  - document.body.clientTop
         };
        }

        document.onmousemove = mouseMove;

        function M(id)
        {
            return document.getElementById(id);//用M()方法代替document.getElementById(id)
        }
        function MC(t)
        {
           return document.createElement(t);//用MC()方法代替document.createElement(t)
        };
        //判断浏览器是否为IE
        function isIE()
        {
              return (document.all && window.ActiveXObject && !window.opera) ? true : false;
        }
        //取得页面的高宽
        function getBodySize()
        {
           var bodySize = [];
           with(document.documentElement) {
            bodySize[0] = (scrollWidth>clientWidth)?scrollWidth:clientWidth;//如果滚动条的宽度大于页面的宽度，取得滚动条的宽度，否则取页面宽度
            bodySize[1] = (scrollHeight>clientHeight)?scrollHeight:clientHeight;//如果滚动条的高度大于页面的高度，取得滚动条的高度，否则取高度
           }
           return bodySize;
        }
        //创建遮盖层
        function popCoverDiv()
        {
           if (M("cover_div")) 
           {
           //如果存在遮盖层，则让其显示
            M("cover_div").style.display = 'block';
           } 
           else 
           {
           //否则创建遮盖层
            var coverDiv = MC('div');
            document.body.appendChild(coverDiv);
            coverDiv.id = 'cover_div';
            with(coverDiv.style)
            {
                 position = 'absolute';
                 background = '#B2B2B2';
                 left = '0px';
                 top = '0px';
                 var bodySize = getBodySize();
                 width = bodySize[0] + 'px'
                 height = bodySize[1] + 'px';
                 zIndex = 0;
                 if (isIE())
                 {
                  filter = "Alpha(Opacity=60)";//IE逆境
                 } else {
                  opacity = 0.6;
                 }
            }
           }
        }

        function showLogin(eid)
        {
            var login=M(eid);
            login.style.display = "block";
        }

        //设置DIV层的样式
        function change(eid)
        {
              var login = M(eid);
              login.style.position = "absolute";
              login.style.border = "1px solid #CCCCCC";
              login.style.background ="#F6F6F6";
              var i=0;
                  var bodySize = getBodySize();
//              login.style.left = (bodySize[0]-i*i*4)/2+"px";
//              login.style.top = (bodySize[1]/2-100-i*i)+"px";
              login.style.left =30+"px"; //x;
              login.style.top = 30+"px";//y;
              login.style.width =500 + "px";
              login.style.height = 200+ "px";
              
              //popChange(i,eid);
        }
        //让DIV层大小循环增大
        function popChange(i,devid)
        {
              var login = M(devid);
                  var bodySize = getBodySize();
              login.style.left = x;//(bodySize[0]-i*i*4)/2+"px";
              login.style.top = y;//(bodySize[1]/2-100-i*i)+"px";
              login.style.width =50+ "px";
              login.style.height = i*i*1+ "px";
              
              if(i<=10){
                   i++;
                   setTimeout("popChange("+i+","+devid+")",10);//设置超时10毫秒
              }
        }
        //打开DIV层
        function opendiv(divid)
        {
                change(divid);
                showLogin(divid);
                //popCoverDiv()
                //void(0);//不进行任何操作,如：<a href="#">aaa</a>
        }
        //关闭DIV层
        function closediv(divid)
        {
                 M(divid).style.display = 'none';
                 //M("cover_div").style.display = 'none';
                void(0);
        }
        function ShowFJ(obj)
        {
            //alert(obj);
            //var DivID = document.getElementById(grd_fj[Number(obj)]).id;
            opendiv(obj);
        }
        function CloseFJ(obj)
        {
            var DivID = document.getElementById(grd_fj[Number(obj)]).id;
            closediv(DivID);
        }