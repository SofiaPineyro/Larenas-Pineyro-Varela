import { Injectable, EventEmitter } from '@angular/core';

@Injectable()
export class Events {
    LoggedStateChanged: EventEmitter<Boolean> = new EventEmitter();
}