// import React, { useRef, useState } from "react";
// import { Toast } from "primereact/toast";
// import { FileUpload } from "primereact/fileupload";

// const Upload = ({ onUploadSuccess }: { onUploadSuccess: (file: File) => void }) => {
//   const toast = useRef<Toast>(null);
//   const [loading, setLoading] = useState(false);

//   const handleFileUpload = async (event: any) => {
//     try {
//       setLoading(true);
//       const file = event.files[0];

//       if (file) {
//         // Check file size
//         if (file.size > 2 * 1024 * 1024) {
//           toast.current?.show({
//             severity: "error",
//             summary: "File Too Large",
//             detail: "The file size exceeds the 2MB limit.",
//             life: 3000,
//           });
//           return;
//         }

//         // Pass the file to the parent handler
//         onUploadSuccess(file);

//         toast.current?.show({
//           severity: "success",
//           summary: "Success",
//           detail: "File uploaded successfully.",
//           life: 3000,
//         });
//       }
//     } catch (error) {
//       toast.current?.show({
//         severity: "error",
//         summary: "Upload Failed",
//         detail: "Something went wrong while uploading the file.",
//         life: 3000,
//       });
//     } finally {
//       setLoading(false);
//     }
//   };

//   return (
//     <div className="card flex justify-content-center">
//       <Toast ref={toast} />
//       <FileUpload
//         mode="basic"
//         accept="image/*"
//         maxFileSize={2 * 1024 * 1024}
//         uploadHandler={handleFileUpload}
//         disabled={loading}
//         chooseLabel="Select Image"
//         uploadLabel="Upload"
//       />
//     </div>
//   );
// };

// export default Upload;
