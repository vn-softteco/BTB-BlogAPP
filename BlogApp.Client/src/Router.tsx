import { createBrowserRouter, Outlet, RouterProvider } from 'react-router-dom'
import { ROUTES } from './utils/constants'

import { SignInPage,
    RequireAuth,
    BlogPostsPage,
    BlogPostsDetailsPage,
    AddBlogPostPage,
    AddCommentPage
} from '@/pages'


const router = createBrowserRouter([
    {
        path: ROUTES.SIGN_IN,
        element: <SignInPage />
    },
    {
        element: (
            <RequireAuth>
                <Outlet />
            </RequireAuth>
        ),
        path: ROUTES.INITIAL_ROUTE,
        children:[
            {
                path: ROUTES.GET_BLOGPOSTs,
                element: <BlogPostsPage />,
            },
            {
                path: ROUTES.BLOGPOST_BY_ID,
                element: <BlogPostsDetailsPage />,
            },
            {
                path: ROUTES.ADD_BLOGPOST,
                element: <AddBlogPostPage />,
            },
            {
                path: ROUTES.ADD_COMMENT,
                element: <AddCommentPage />,
            },
        ],

    }
]);

function Router() {
    return <RouterProvider router={router} />
}
  
export default Router