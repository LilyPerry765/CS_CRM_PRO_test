declare var Morris: any;

namespace CaseManagement.StimulReport {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.resizable()
    @Serenity.Decorators.maximizable()


    var Groups = new Array();

    var Provinces = new Array();

    var ProvinceActivities = new Array();

    //var PROVINCEACTIVITYCOUNT = 0;

   
    export class ProvinceActivityReport extends Serenity.TemplatedDialog<any> {

        private areaChart: any;
       
        static ProvinceActivityGroupList() {

            var SetactivityGroupList = CaseManagement.Case.ActivityService.ActivitybyGroupList({}, response => {

                var combo = document.getElementById("ActivityGroup");

                for (var i = 0; i < response.Values.length; i++) {
                  
                    Groups.push(new ActivityGroup(response.Values[i].GroupId, response.Values[i].Code,response.Values[i].Name));
                    //   console.log("ID :" + Groups[i]['GroupId'] + "Code :" + Groups[i]['Codes']);

                    var option = document.createElement("option");
                    option.text = response.Values[i].GroupName;
                    option.value = response.Values[i].GroupId;

                    combo.add(option, null); //Standard 
                }

            });

            let Excludecols: [string, string, string, string, string, string, string, string, string, string, string, string, string, string];
            Excludecols = ["EnglishName", "ManagerName", "LetterNo", "PmoLevelId", "InstallDate", "CreatedUserId", "CreatedDate", "ModifiedUserId", "ModifiedDate",
                "IsDeleted", "DeletedUserId", "'DeletedDate", "PmoLevelName", "LeaderName"];

            var SetProvinceList = CaseManagement.Case.ProvinceService.List({ ExcludeColumns: Excludecols, "Sort": ["Id"]  }, response => {

                for (var i = 0; i < response.TotalCount; i++) {

                    Provinces.push(new Province(response.Entities[i].Id, response.Entities[i].Name));
                    //console.log("ProvinceId : " + response.Entities[i].Id + " ProvinceName : " + response.Entities[i].Name );
                }
            });

    
        }


        static ProvinceActivityList(GID) {

           var PROVINCEACTIVITYCOUNT = 0;

            var GroupCodes = Groups[GID - 1]['Codes'];

            var GroupNames = Groups[GID - 1]['Names'];

           // var P_Avtivity = _.groupBy(ProvinceActivity, 'ProvinceId', 'Code');

            //console.log(GroupCodes.length);

            //console.log(GroupNames.length);

            ProvinceActivities = [];
    
            let Excludecols: [string, string, string];
            Excludecols = [ "CycleCost", "Factor", "DelayedCost", "YearCost", "AccessibleCost", "InaccessibleCost", "Financial", "TotalLeakage", "RecoverableLeakage",
                "Recovered", "EventDescription", "MainReason", "CorrectionOperation", "AvoidRepeatingOperation", "CreatedUserId", "CreatedDate", "ModifiedUserId", "ModifiedDate"
                , "IsDeleted", "CycleId", "CustomerSelectedId", "CustomerEffectId", "OrganizationEffectId", "IncomeFlowId", "RiskLevelId", "StatusID", "DiscoverLeakDate", "ActionId", "ConfirmTypeID"
                , "CommentReasonList"];
            
            var aaa = CaseManagement.Case.ActivityRequestService.List({ExcludeColumns:Excludecols}, response => {

                for(var RequestCounter = 0; RequestCounter < response.TotalCount; RequestCounter++)
                {  

                    if(String(response.Entities[RequestCounter].ActivityCode).charAt(1) ===  String(GID) )
                    {   
                        
                        ProvinceActivities.push(new ProvinceActivity(response.Entities[RequestCounter].ProvinceId, response.Entities[RequestCounter].ActivityCode,1));

                    }
                }

               // console.log("ProvinceActivities length : " + ProvinceActivities.length);

              /*  var unique = ProvinceActivities.filter(function (elem, index, self) {
                    return index == self.indexOf(elem);
                });
                console.log("Length after Process : " + unique.length);
                */

               /* for (var PAcounter = 0; PAcounter < ProvinceActivities.length; PAcounter++)
                {

                }*/

                 //console.log(response.TotalCount);
                var TableHead = document.getElementById('Province_Activity_Table').getElementsByTagName('thead')[0];

                var TableBody = document.getElementById('Province_Activity_Table').getElementsByTagName('tbody')[0];


                TableHead.innerHTML = '';
                TableBody.innerHTML = '';

                var newRow = TableHead.insertRow(TableHead.rows.length);

                var th = document.createElement('th');
                th.style = "text-align:center;font-size:11px;";
                th.innerHTML = 'استان';
                newRow.appendChild(th);
                
                for(var GCcounter = 0; GCcounter < GroupCodes.length; GCcounter++) {

                    var th = document.createElement('th');
                    th.style = "text-align:center";
                    th.innerHTML = GroupCodes[GCcounter];
                    newRow.appendChild(th);

                }
              
                for (var counter = 0; counter < Provinces.length; counter++) {
                    //console.log("PROVINCE LOOP");
                    //PROVINCEACTIVITYCOUNT = 0;

                    var newRow1 = TableBody.insertRow(TableBody.rows.length);
                    var newCell = newRow1.insertCell(0);

                    // Append a text node to the cell
                    newCell.style = "text-align:center,font-size:11px;";
                    var newText = document.createTextNode(Provinces[counter]['Name']);
                    newCell.appendChild(newText);
                   
                    newRow1.appendChild(newCell);

                    for(var counter1 = 0; counter1 < GroupCodes.length; counter1++) {
                      //  console.log("GROUPCODES");
                        var cellIndex = counter1 + 1;
                        var newCell1 = newRow1.insertCell(cellIndex);

                        // Append a text node to the cell
                        newCell1.style = "text-align:center;font-size:13px;font-weight: bold";
                        var flag = false;

                    
                        var PActivitiesCounter = 0;
                        for (var counter2 = 0; counter2 < ProvinceActivities.length; counter2++) {
                            //console.log("PROVINCEACTIVITY LOOP");
                            //console.log("Province : " + ProvinceActivities[counter2]['ProvinceId'] + ' --- ' + Provinces[counter]['Id']);
                           // console.log("Activity : " + ProvinceActivities[counter2]['Code'].length + ' --- ' + GroupCodes[counter1].length + ' --- After Trim : ' + GroupCodes[counter1].trim().length);
                                if ((ProvinceActivities[counter2]['Code'] == GroupCodes[counter1].trim()) && (ProvinceActivities[counter2]['ProvinceId'] == Provinces[counter]['Id'])) {
                                    PActivitiesCounter += 1;
                                   
                                    var arabicNumbers = ['۰', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩'];

                                    var chars = PActivitiesCounter.toString().split('');

                                    for (var i = 0; i < chars.length; i++) {
                                        if (/\d/.test(chars[i])) {
                                            chars[i] = arabicNumbers[chars[i]];
                                        }
                                    }

                                    var count = chars.join('');

                                   
                                    var newText1 = document.createTextNode(count.toString());

                                    newCell1.setAttribute('id', ProvinceActivities[counter2]['ProvinceId'] + "_" + GroupCodes[counter1]);

                                    newCell1.title = GroupNames[counter1];

                                    flag = true;
                                }
                            
                     }
                        PROVINCEACTIVITYCOUNT = 0;
                        if (!flag) {
                            var newText1 = document.createTextNode("");
                        }

                        newCell1.appendChild(newText1);

                        newRow1.appendChild(newCell1);
                    }

                }

            });

        }
    }
      class ActivityGroup {

        GroupId: number;
        Codes: string[];
        Names: string[];

        constructor(public Id, public code, public name) {
            this.GroupId = Id;
            this.Codes = code;
            this.Names = name;
        }

    }

    class Province {

        Id: number;
        Name: string;

        constructor(public ID, public name) {
            this.Id = ID;
            this.Name = name;
        }

    }


    class ProvinceActivity {

        ProvinceId: number;
        Code: string;
        Count: number;

        constructor(public Id, public code, public count ) {
            this.ProvinceId = Id;
            this.Code = code;
            this.Count = count;
        }

    }
}