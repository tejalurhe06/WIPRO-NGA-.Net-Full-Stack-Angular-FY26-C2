import { CanActivateFn } from '@angular/router';
import { StorageUtil } from '../../shared/utils/storage.util';

export const roleGuard = (roles: string[]): CanActivateFn => () => {
  const role = StorageUtil.getItem('role');
  return role ? roles.includes(role) : false;
};
