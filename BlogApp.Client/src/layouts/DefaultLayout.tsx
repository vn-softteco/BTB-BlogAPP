import Header from '@/components/Header/Header'

export const DefaultLayout:React.FC<React.PropsWithChildren<unknown>> = ({ children }) => (
    <>
        <Header />
        {children}
    </>
);
