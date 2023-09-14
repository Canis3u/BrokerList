import { BrokerService } from './api-info/brokerlist-api';
import { Component, OnInit } from '@angular/core';
import { BrokerRespViewModel } from './api-info/brokerlist-api/model/brokerRespViewModel';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-page-brokerlist',
  templateUrl: './page-brokerlist.component.html',
  styleUrls: ['./page-brokerlist.component.css']
})
export class PageBrokerlistComponent implements OnInit{
  displayedColumns: string[] = ['證券商代號', '證券商名稱', '開業日', '地址', '電話', '刪除']
  displayedBrokerItems: BrokerRespViewModel[] = []
  headquarterCode: string = ""
  startDateString: string = ""
  endDateString: string = ""
  updateButtonDisabled: boolean = false
  constructor(
    private confirmationService:ConfirmationService,
    private brokerService:BrokerService,
  ){}

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.GetBrokerList()
  }

  InitSelect(){
    this.headquarterCode = ""
    this.startDateString = ""
    this.endDateString = ""
    this.updateButtonDisabled = false
  }

  GetBrokerList(){
    this.brokerService.apiBrokerGet(this.headquarterCode,this.startDateString,this.endDateString).subscribe((data)=>{
      this.displayedBrokerItems = data
    })
  }

  ConfirmDelete(code?:string) {
    this.confirmationService.confirm({
        message: 'Are you sure that you want to delete this?',
        accept: () => {
          this.DeleteByCode(code)
        }
    });
  }
  ConfirmDeleteAll(code?:string) {
    this.confirmationService.confirm({
        message: 'Are you sure that you want to delete all?',
        accept: () => {
          this.DeleteByCode('All')
        }
    });
  }

  DeleteByCode(code?:string){
    if(code==null) return
    this.brokerService.apiBrokerDeleteCodeDelete(code).subscribe(() => {
      this.InitSelect()
      this.GetBrokerList()
    });
  }

  UpdateBrokerList(){
    this.updateButtonDisabled = true
    this.brokerService.apiBrokerPost().subscribe(() => {
      this.InitSelect()
      this.GetBrokerList()
      this.updateButtonDisabled = false
    });
  }

  IsHeadquarter(code?:string): boolean {
    if (code==null) return false
    return code[3]=='0'
  }

}
