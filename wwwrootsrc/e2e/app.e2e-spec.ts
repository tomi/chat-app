import { WwwrootsrcPage } from './app.po';

describe('wwwrootsrc App', () => {
  let page: WwwrootsrcPage;

  beforeEach(() => {
    page = new WwwrootsrcPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
