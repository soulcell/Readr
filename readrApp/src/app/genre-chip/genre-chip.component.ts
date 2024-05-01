import { Component, EventEmitter, HostBinding, Input, Output } from '@angular/core';
import { IonChip } from '@ionic/angular/standalone';
@Component({
  selector: 'app-genre-chip',
  templateUrl: './genre-chip.component.html',
  styleUrls: ['./genre-chip.component.scss'],
  standalone: true,
  imports: [IonChip]
})
export class GenreChipComponent {

  @Input()
  title!: string;

  @HostBinding('class.checked')
  isChecked: boolean = false;

  @Output()
  toggledEvent = new EventEmitter<boolean>()

  constructor() { }

  toggle() {
    this.isChecked = !this.isChecked

    this.toggledEvent.emit(this.isChecked);
  }

}
