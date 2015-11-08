 function SetPermissionToHiddenForm()
    {
       clientPermission_ = document.getElementById("clientPermission");
       permissionTable_ = document.getElementById("permissionTable");
       permission_ = "";
       for(i=1;i<permissionTable_.rows.length;i++)
       {
          row_ = permissionTable_.rows[i];
          
          
          for(j=0;j<row_.cells.length;j++)
          {          
            cell_=row_.cells[j]; 
            checks_ = cell_.getElementsByTagName("input");                    
            if(checks_.length==0){ continue;}
            for(k=0;k<checks_.length;k++)
            {
                check_ = checks_[k];
                if(check_.checked == true)
                {
                    permission_ += "1";
                }
                else                
                {
                    permission_ += "0";
                }
            }            
          }
          permission_ += "|";
          
       }
       
       clientPermission_.value = permission_.substr(0,permission_.length-1);
    }    
