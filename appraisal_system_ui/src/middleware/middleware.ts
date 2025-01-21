import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';
import jwt from 'jsonwebtoken'; 

const SECRET_KEY = process.env.JWT_SECRET_KEY || 'GSf2NJ/5tOA5It0JEKWgKJs+SIIPUY8PbxBpme9jllV9vfU+4442sHK21gkwdXBL1p4495uLyXGA2HlbHh26KQ=='; 

export function middleware(request: NextRequest) {
  const authToken = request.headers.get('Authorization')?.replace('Bearer ', '');

  const protectedRoutes = ['/ProgressPoint'];

  if (protectedRoutes.includes(request.nextUrl.pathname)) {
    if (!authToken) {
      return NextResponse.redirect(new URL('/', request.url));
    }

    try {
      const decoded = jwt.verify(authToken, SECRET_KEY) as jwt.JwtPayload;

      const currentTime = Math.floor(Date.now() / 1000);
      if (decoded.exp && decoded.exp < currentTime) {
        return NextResponse.redirect(new URL('/', request.url));
      }
    } catch (err) {
      return NextResponse.redirect(new URL('/', request.url)); 
    }
  }

  return NextResponse.next();
}

export const config = {
  matcher: ['/ProgressPoint/'],
};
