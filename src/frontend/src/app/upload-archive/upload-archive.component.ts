import { Component } from '@angular/core';
import { ArchiveService } from '../services/archive.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-upload-archive',
  templateUrl: './upload-archive.component.html',
  styleUrl: './upload-archive.component.css'
})
export class UploadArchiveComponent {
  fileName = '';

  constructor(
    private archiveService: ArchiveService,
    private messageService: MessageService) { }

  onFileSelected(event) {
    if (!event)
      return;

    const file: File = event.target.files[0];
    console.log(file);
    this.fileName = file.name;
    // todo: добавить валидацию расширения архива

    this.archiveService.upload(file)
        .subscribe(_ => this.messageService.add("Архив был успешно загружен."));
  }
}