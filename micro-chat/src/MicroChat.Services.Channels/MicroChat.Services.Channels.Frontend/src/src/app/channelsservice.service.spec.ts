import { TestBed } from '@angular/core/testing';

import { ChannelsserviceService } from './channelsservice.service';

describe('ChannelsserviceService', () => {
  let service: ChannelsserviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ChannelsserviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
