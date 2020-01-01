import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-deadline',
  templateUrl: './deadline.component.html',
  styleUrls: ['./deadline.component.css']
})
export class DeadlineComponent implements OnInit {
    public data: string[];
    constructor(private api: ApiService) { }

    ngOnInit() {
        this.data = this.api.data;
  }

}
